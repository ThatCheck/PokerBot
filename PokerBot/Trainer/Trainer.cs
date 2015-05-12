using HandHistories.Objects.GameDescription;
using HandHistories.Objects.Hand;
using HandHistories.Parser.Parsers.Base;
using HandHistories.Parser.Parsers.Factory;
using NLog;
using PokerBot.CaseBased.Base;
using PokerBot.CaseBased.PostFlop;
using PokerBot.CaseBased.PreFlop;
using PokerBot.CaseBased.Trainer;
using PokerBot.CustomForm.Event;
using PokerBot.Entity.Table;
using PokerBot.Postgres;
using Smile;
using Smile.Learning;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokerBot.Trainer
{
    public class Trainer
    {

        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private int _numberEstimatedTrainingHand = 0;

        public delegate void ProgressEventHandler(object sender, ProgressEventArgs data);
        public event ProgressEventHandler ProgressEvent;

        public int NumberEstimatedTrainingHand
        {
            get { return _numberEstimatedTrainingHand; }
        }

        public Trainer()
        {

        }

        public void extractDataPlayer(String listPlayerFile, String outputFile)
        {
            Object locker = new Object();
            using (StreamWriter stream = new StreamWriter(outputFile))
            {
                using (StreamReader streamReader = new StreamReader(listPlayerFile))
                {
                    String[] line = streamReader.ReadToEnd().Split(new String[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    this._numberEstimatedTrainingHand = line.Count();
                    _logger.Info("Extract " + line.Count() + "players(s)");
                    int numberCurrentSend = 0;
                    ParallelOptions po = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = Environment.ProcessorCount
                    };
                    Parallel.ForEach(line, po, player =>
                    {
                        String dataName = player.Split(';')[1];
                        try
                        {
                            dataName = dataName.Replace("\"", "");
                            Player playerInstance = new Player();
                            playerInstance.Name = dataName;
                            var stats = playerInstance.Stats;
                            lock (locker)
                            {
                                stream.WriteLine(playerInstance.Name + ";" + stats.Vpip + ";" + stats.Pfr + ";" + stats.ThreeBetPF + ";" + stats.FoldToThreeBet);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.Warn("Unable to collect data for : " + dataName + "\n ERROR : " + ex.Message + "\n Trace : " + ex.StackTrace);
                        }
                        finally 
                        {
                            Interlocked.Increment(ref numberCurrentSend);
                            this.OnRaiseProgressEvent(new ProgressEventArgs(numberCurrentSend, _numberEstimatedTrainingHand));
                        }
                    });
                }
            }
        }

        public decimal[] testTrain(String networkPath, String trainingPath, SiteName site)
        {
            _logger.Info("Training ON => " + trainingPath);
            int[] found = new int[3] { 0, 0, 0 };
            int[] foundHandGroup = new int[3] { 0, 0, 0 };
            int[] foundHandTypeFlop = new int[3] { 0, 0, 0 };
            int[] foundHandTypeTurn = new int[3] { 0, 0, 0 };
            
            Object locker = new Object();
            IEnumerable<HandHistory> listHand = endcodeHandFromFile(trainingPath, site);
            this._numberEstimatedTrainingHand = 0;
            foreach (var hand in listHand)
            {
                foreach (var player in hand.Players)
                {
                    if (player.hasHoleCards)
                    {
                        this._numberEstimatedTrainingHand++;
                    }
                }
            }
            BayesianNetwork.V1.Network net = new BayesianNetwork.V1.Network(networkPath);
            _logger.Info("Extract " + listHand.Count() + "hand(s)");
            int numberCurrentSend = 0;
            this._numberEstimatedTrainingHand = 0;
            foreach (var hand in listHand)
            {
                foreach (var player in hand.Players)
                {
                    if (player.hasHoleCards)
                    {
                        this._numberEstimatedTrainingHand++;
                    }
                }
            }
            ParallelOptions po = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };
            foreach(HandHistory hand in listHand)
            {
                //Decode the table
                Table table = new Table(hand);
                foreach (var player in hand.Players)
                {
                    if (player.hasHoleCards)
                    {
                        try
                        {
                            Stopwatch watch = Stopwatch.StartNew();
                            Player selectedPlayer = table.ListPlayer.Where(p => p.Name == player.PlayerName).First();
                            if (selectedPlayer == null)
                            {
                                throw new InvalidDataException("Unable to load player");
                            }
                            var dataResultHandGroup = net.getValueForHandType(table, selectedPlayer, HandHistories.Objects.Cards.Street.Preflop);
                            var dataResultFlop = net.getValueForHandType(table, selectedPlayer, HandHistories.Objects.Cards.Street.Flop);
                            var dataResultTurn = net.getValueForHandType(table, selectedPlayer, HandHistories.Objects.Cards.Street.Turn);
                            var dataResultRiver = net.getValueForHandType(table, selectedPlayer);

                            BayesianNetwork.V1.HandType.HandTypeRiver handTypeRiver = new BayesianNetwork.V1.HandType.HandTypeRiver(table.Board, selectedPlayer.Hand);
                            BayesianNetwork.V1.HandType.HandType handTypeFlop = new BayesianNetwork.V1.HandType.HandType(table.Board, selectedPlayer.Hand);
                            BayesianNetwork.V1.HandType.HandTypeTurn handTypeTurn = new BayesianNetwork.V1.HandType.HandTypeTurn(table.Board, selectedPlayer.Hand);
                            BayesianNetwork.V1.HandGroup.HandGroup handGroup = new BayesianNetwork.V1.HandGroup.HandGroup(selectedPlayer.Hand);

                            if (dataResultRiver.ElementAt(0).Key.Equals(handTypeRiver.ToString()))
                            {
                                found[0]++;
                            }
                            else if (dataResultRiver.ElementAt(1).Key.Equals(handTypeRiver.ToString()))
                            {
                                found[1]++;
                            }
                            else if (dataResultRiver.ElementAt(2).Key.Equals(handTypeRiver.ToString()))
                            {
                                found[2]++;
                            }

                            if (dataResultTurn.ElementAt(0).Key.Equals(handTypeTurn.ToString()))
                            {
                                foundHandTypeTurn[0]++;
                            }
                            else if (dataResultTurn.ElementAt(1).Key.Equals(handTypeTurn.ToString()))
                            {
                                foundHandTypeTurn[1]++;
                            }
                            else if (dataResultTurn.ElementAt(2).Key.Equals(handTypeTurn.ToString()))
                            {
                                foundHandTypeTurn[2]++;
                            }

                            if (dataResultFlop.ElementAt(0).Key.Equals(handTypeFlop.ToString()))
                            {
                                foundHandTypeFlop[0]++;
                            }
                            else if (dataResultFlop.ElementAt(1).Key.Equals(handTypeFlop.ToString()))
                            {
                                foundHandTypeFlop[1]++;
                            }
                            else if (dataResultFlop.ElementAt(2).Key.Equals(handTypeFlop.ToString()))
                            {
                                foundHandTypeFlop[2]++;
                            }

                            if (dataResultHandGroup.ElementAt(0).Key.Equals(handGroup.ToString()))
                            {
                                foundHandGroup[0]++;
                            }
                            else if (dataResultHandGroup.ElementAt(1).Key.Equals(handGroup.ToString()))
                            {
                                foundHandGroup[1]++;
                            }
                            else if (dataResultHandGroup.ElementAt(2).Key.Equals(handGroup.ToString()))
                            {
                                foundHandGroup[2]++;
                            }
                            watch.Stop();
                            //_logger.Info("[TIME : " + watch.ElapsedMilliseconds + " ] : Training ok for player " + player.PlayerName + " and ID HAND " + hand.HandId);
                        }
                        catch (Exception ex)
                        {
                            _logger.Warn("Unable to train network for data : " + hand.FullHandHistoryText + "\n ERROR : " + ex.Message + "\n Trace : " + ex.StackTrace);
                        }
                        finally
                        {
                            Interlocked.Increment(ref numberCurrentSend);
                            this.OnRaiseProgressEvent(new ProgressEventArgs(numberCurrentSend, _numberEstimatedTrainingHand));
                        }
                    }
                }
            }
            var returnValue = new decimal[] { ((decimal)found[0] / (decimal)numberCurrentSend) * 100, ((decimal)found[1] / (decimal)numberCurrentSend) * 100, ((decimal)found[2] / (decimal)numberCurrentSend) * 100 };
            var returnValueFlop = new decimal[] { ((decimal)foundHandTypeFlop[0] / (decimal)numberCurrentSend) * 100, ((decimal)foundHandTypeFlop[1] / (decimal)numberCurrentSend) * 100, ((decimal)foundHandTypeFlop[2] / (decimal)numberCurrentSend) * 100 };
            var returnValueTurn = new decimal[] { ((decimal)foundHandTypeTurn[0] / (decimal)numberCurrentSend) * 100, ((decimal)foundHandTypeTurn[1] / (decimal)numberCurrentSend) * 100, ((decimal)foundHandTypeTurn[2] / (decimal)numberCurrentSend) * 100 };
            var returnValuePreFlop = new decimal[] { ((decimal)foundHandGroup[0] / (decimal)numberCurrentSend) * 100, ((decimal)foundHandGroup[1] / (decimal)numberCurrentSend) * 100, ((decimal)foundHandGroup[2] / (decimal)numberCurrentSend) * 100 };
            _logger.Info("Resultat For River: " + returnValue[0] + " ; " + returnValue[1] + " ; " + returnValue[2]);
            _logger.Info("Resultat For Turn: " + returnValueTurn[0] + " ; " + returnValueTurn[1] + " ; " + returnValueTurn[2]);
            _logger.Info("Resultat For Flop: " + returnValueFlop[0] + " ; " + returnValueFlop[1] + " ; " + returnValueFlop[2]);
            _logger.Info("Resultat For PreFlop: " + returnValuePreFlop[0] + " ; " + returnValuePreFlop[1] + " ; " + returnValuePreFlop[2]);
            net.Dispose();
            return returnValue;
        }

        public void train(String networkPath, String trainingPath)
        {
            using (Network net = new Network())
            {
                try
                {
                    _logger.Info("Start training");
                    net.ReadFile(networkPath);
                    using (DataSet dataSet = new DataSet())
                    {
                        dataSet.ReadFile(trainingPath);
                        DataMatch[] matches = dataSet.MatchNetwork(net);
                        using (EM em = new EM())
                        {
                            em.Learn(dataSet, net, matches);
                        }
                    }
                    net.WriteFile(networkPath);
                    _logger.Info("TRAINING OK ! ");
                }
                catch (Exception ex)
                {
                    _logger.Error("Unable to train network : " + ex.Message + "\n " + ex.Message);
                    throw;
                }
            }
        }

        public void createBayesianNetwork(String name)
        {
            _logger.Info("Create the BayesianNetwork : " + name);
            using (Network net = new Network())
            {
                List<Type> networkListClass = BayesianNetwork.V1.Network.getAllClassForNetworkCreation();
                Dictionary<String, int> nameClassAlreadyAdd = new Dictionary<String, int>();
                foreach (Type addingClass in networkListClass)
                {
                    int value = net.AddNode(Network.NodeType.Cpt, addingClass.Name);
                    String[] data = ((String[])addingClass.GetMethod("getValueName", BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static).Invoke(null, null)).Distinct().ToArray();
                    foreach (String outcome in data)
                    {
                        net.AddOutcome(addingClass.Name, outcome);
                    }
                    net.DeleteOutcome(addingClass.Name, 0);
                    net.DeleteOutcome(addingClass.Name, 0);
                    nameClassAlreadyAdd.Add(addingClass.Name, value);
                    _logger.Info("ADD NODE : " + addingClass.Name);
                }

                foreach (Type addingClass in networkListClass)
                {
                    String[] arcString = (String[])addingClass.GetMethod("getArcForValue", BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static).Invoke(null, null);
                    foreach (String arc in arcString)
                    {
                        net.AddArc(addingClass.Name, arc);
                        _logger.Info("ADD ARC FROM " + addingClass.Name + " TO " + arc);
                    }
                }

                net.WriteFile(name);
            }
        }

        public void generateCBRPreFlopRange(String[] inputHand, String output, SiteName site)
        {
            _logger.Info("Start Training !");
            _logger.Info("Generate header ! ");
            Object locker = new Object();
            Object lockerIntIncrement = new Object();
            ConcurrentBag<PreflopCase> bag = new ConcurrentBag<PreflopCase>();
            int numberCurrentSend = 0;
            this._numberEstimatedTrainingHand = inputHand.Count();
            Parallel.ForEach(inputHand, pathInput =>
            {
                _logger.Info("Training ON => " + pathInput);
                IEnumerable<HandHistory> listHand = endcodeHandFromFile(pathInput, site);
                _logger.Info("Extract " + listHand.Count() + "hand(s)");
                List<PreflopCase> data = new List<PreflopCase>();
                foreach (HandHistory hand in listHand)
                {
                    try
                    {
                        Table table = new Table(hand);
                        foreach (var player in hand.Players)
                        {
                            if (player.hasHoleCards 
                                && hand.HandActions.Any( p => p.PlayerName == player.PlayerName && p.Street == HandHistories.Objects.Cards.Street.Preflop)
                                && hand.HandActions.Any(p => p.PlayerName == player.PlayerName && p.Street == HandHistories.Objects.Cards.Street.Flop)
                                && hand.HandActions.Any(p => p.PlayerName == player.PlayerName && p.Street == HandHistories.Objects.Cards.Street.Turn)
                                && hand.HandActions.Any(p => p.PlayerName == player.PlayerName && p.Street == HandHistories.Objects.Cards.Street.River)
                                )
                            {
                                    Player selectedPlayer = table.ListPlayer.Where(p => p.Name == player.PlayerName).First();
                                    if (selectedPlayer == null)
                                    {
                                        throw new InvalidDataException("Unable to load player");
                                    }
                                    data.AddRange(TrainerCase.generatePreFlopCaseForHand(hand, new List<Player>() { selectedPlayer }));
                                
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn("Unable to train network for data : " + hand.FullHandHistoryText + "\n ERROR : " + ex.Message + "\n Trace : " + ex.StackTrace);
                    }
                }
                foreach(PreflopCase pfCase in data)
                {
                    bag.Add(pfCase);
                }
                Interlocked.Increment(ref numberCurrentSend);
                this.OnRaiseProgressEvent(new ProgressEventArgs(numberCurrentSend, _numberEstimatedTrainingHand));
            });
            QBRBase qbr = new QBRBase();
            qbr.fromIEnumerable(bag);
            qbr.serialize(output);
        }

        public void generateCBRPostFlopDecision(String[] inputHand, String output, string networkPath,SiteName site)
        {
            _logger.Info("Start Training !");
            _logger.Info("Generate header ! ");
            Object locker = new Object();
            Object lockerIntIncrement = new Object();
            ConcurrentBag<PostFlopDecisionCase> bag = new ConcurrentBag<PostFlopDecisionCase>();
            int numberCurrentSend = 0;
            this._numberEstimatedTrainingHand = inputHand.Count();
            Parallel.ForEach(inputHand, pathInput =>
            {
                _logger.Info("Training ON => " + pathInput);
                IEnumerable<HandHistory> listHand = endcodeHandFromFile(pathInput, site);
                _logger.Info("Extract " + listHand.Count() + "hand(s)");
                List<PostFlopDecisionCase> data = new List<PostFlopDecisionCase>();
                foreach (HandHistory hand in listHand)
                {
                    try
                    {
                        Table table = new Table(hand);
                        foreach (var player in hand.Players)
                        {
                            if (player.hasHoleCards
                                && hand.HandActions.Any(p => p.PlayerName == player.PlayerName && p.Street == HandHistories.Objects.Cards.Street.Preflop)
                                && hand.HandActions.Any(p => p.PlayerName == player.PlayerName && p.Street == HandHistories.Objects.Cards.Street.Flop)
                                && hand.HandActions.Any(p => p.PlayerName == player.PlayerName && p.Street == HandHistories.Objects.Cards.Street.Turn)
                                && hand.HandActions.Any(p => p.PlayerName == player.PlayerName && p.Street == HandHistories.Objects.Cards.Street.River)
                                )
                            {
                                Player selectedPlayer = table.ListPlayer.Where(p => p.Name == player.PlayerName).First();
                                if (selectedPlayer == null)
                                {
                                    throw new InvalidDataException("Unable to load player");
                                }
                                data.AddRange(TrainerCase.generatePostFlopDecisionCaseForHand(hand, table, new List<Player>() { selectedPlayer }));

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn("Unable to train network for data : " + hand.FullHandHistoryText + "\n ERROR : " + ex.Message + "\n Trace : " + ex.StackTrace);
                    }
                }
                foreach (PostFlopDecisionCase pfCase in data)
                {
                    bag.Add(pfCase);
                }
                Interlocked.Increment(ref numberCurrentSend);
                this.OnRaiseProgressEvent(new ProgressEventArgs(numberCurrentSend, _numberEstimatedTrainingHand));
            });
            QBRBase qbr = new QBRBase();
            qbr.fromIEnumerable(bag);
            qbr.serialize(output);
        }

        public void generateCBRPreFlopDecision(String[] inputHand, String output, SiteName site)
        {
            _logger.Info("Start Training !");
            _logger.Info("Generate header ! ");
            Object locker = new Object();
            Object lockerIntIncrement = new Object();
            ConcurrentBag<PreflopDecisionCase> bag = new ConcurrentBag<PreflopDecisionCase>();
            int numberCurrentSend = 0;
            this._numberEstimatedTrainingHand = inputHand.Count();
            Parallel.ForEach(inputHand, pathInput =>
            {
                _logger.Info("Training ON => " + pathInput);
                IEnumerable<HandHistory> listHand = endcodeHandFromFile(pathInput, site);
                _logger.Info("Extract " + listHand.Count() + "hand(s)");
                List<PreflopDecisionCase> data = new List<PreflopDecisionCase>();
                foreach (HandHistory hand in listHand)
                {
                    try
                    {
                        Table table = new Table(hand);
                        foreach (var player in hand.Players)
                        {
                            if (player.hasHoleCards
                                && hand.HandActions.Any(p => p.PlayerName == player.PlayerName && p.Street == HandHistories.Objects.Cards.Street.Preflop)
                                && hand.HandActions.Any(p => p.PlayerName == player.PlayerName && p.Street == HandHistories.Objects.Cards.Street.Flop)
                                && hand.HandActions.Any(p => p.PlayerName == player.PlayerName && p.Street == HandHistories.Objects.Cards.Street.Turn)
                                && hand.HandActions.Any(p => p.PlayerName == player.PlayerName && p.Street == HandHistories.Objects.Cards.Street.River)
                                )
                            {
                                Player selectedPlayer = table.ListPlayer.Where(p => p.Name == player.PlayerName).First();
                                if (selectedPlayer == null)
                                {
                                    throw new InvalidDataException("Unable to load player");
                                }
                                data.AddRange(TrainerCase.generatePreFlopDecisionCaseForHand(hand, new List<Player>() { selectedPlayer }));

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn("Unable to train network for data : " + hand.FullHandHistoryText + "\n ERROR : " + ex.Message + "\n Trace : " + ex.StackTrace);
                    }
                }
                foreach (PreflopDecisionCase pfCase in data)
                {
                    bag.Add(pfCase);
                }
                Interlocked.Increment(ref numberCurrentSend);
                this.OnRaiseProgressEvent(new ProgressEventArgs(numberCurrentSend, _numberEstimatedTrainingHand));
            });
            QBRBase qbr = new QBRBase();
            qbr.fromIEnumerable(bag);
            qbr.serialize(output);
        }

        public void generateDataTraining(String[] inputHand, String output,SiteName site)
        {

            _logger.Info("Start Training !");
            _logger.Info("Generate header ! ");
            Object locker = new Object();
            Object lockerIntIncrement = new Object();
            using (StreamWriter stream = new StreamWriter(output))
            {
                stream.WriteLine(string.Join(" ", BayesianNetwork.V1.Network.getHeader()));
                int numberCurrentSend = 0;
                this._numberEstimatedTrainingHand = inputHand.Count();
                Parallel.ForEach(inputHand, pathInput =>
                {
                    _logger.Info("Training ON => " + pathInput);
                    IEnumerable<HandHistory> listHand = endcodeHandFromFile(pathInput, site);
                    _logger.Info("Extract " + listHand.Count() + "hand(s)");
                    List<String> data = new List<string>();
                    foreach(HandHistory hand in listHand)
                    {
                        //Decode the table
                            try
                            {
                            Table table = new Table(hand);
                            foreach (var player in hand.Players)
                            {
                                if (player.hasHoleCards 
                                    && hand.HandActions.Any(p => p.PlayerName == player.PlayerName && p.Street == HandHistories.Objects.Cards.Street.Preflop)
                                    && hand.HandActions.Any(p => p.PlayerName == player.PlayerName && p.Street == HandHistories.Objects.Cards.Street.Flop)
                                    && hand.HandActions.Any(p => p.PlayerName == player.PlayerName && p.Street == HandHistories.Objects.Cards.Street.Turn)
                                    && hand.HandActions.Any(p => p.PlayerName == player.PlayerName && p.Street == HandHistories.Objects.Cards.Street.River))
                                {
                                        //Stopwatch watch = Stopwatch.StartNew();
                                        BayesianNetwork.V1.Network net = new BayesianNetwork.V1.Network();
                                        Player selectedPlayer = table.ListPlayer.Where(p => p.Name == player.PlayerName).First();
                                        if (selectedPlayer == null)
                                        {
                                            throw new InvalidDataException("Unable to load player");
                                        }
                                        net.setTableForTraining(table, selectedPlayer);
                                        //watch.Stop();
                                        //_logger.Info("[TIME : " + watch.ElapsedMilliseconds +" ] : Training ok for player " + player.PlayerName + " and ID HAND " + hand.HandId);
                                        data.Add(string.Join(" ", net.getValue()));
                                
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.Warn("Unable to train network for data : " + hand.FullHandHistoryText + "\n ERROR : " + ex.Message + "\n Trace : " + ex.StackTrace);
                        }
                    }
                    lock (locker)
                    {
                        stream.WriteLine(string.Join(System.Environment.NewLine, data));
                    }
                    Interlocked.Increment(ref numberCurrentSend);
                    this.OnRaiseProgressEvent(new ProgressEventArgs(numberCurrentSend, _numberEstimatedTrainingHand));
                });
            }
        }

        public static IEnumerable<HandHistories.Objects.Hand.HandHistory> endcodeHandFromFile(String path, SiteName sitename)
        {
            IHandHistoryParserFactory handHistoryParserFactory = new HandHistoryParserFactoryImpl();
            IHandHistoryParser parser = handHistoryParserFactory.GetFullHandHistoryParser(sitename);
            ConcurrentBag<HandHistories.Objects.Hand.HandHistory> returnHandHistory = new ConcurrentBag<HandHistories.Objects.Hand.HandHistory>();
            using (StreamReader stream = new StreamReader(path, Encoding.Default, true))
            {
                string[] splittedHand = MemoryReader.MemoryReader._gameStartRegex.Split(stream.ReadToEnd());
                Parallel.ForEach(splittedHand, hand =>
                {
                    if (hand != "")
                    {
                        String formattedNewString = "#Game No :" + hand;
                        formattedNewString = String.Join(Environment.NewLine, formattedNewString.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Where( p => !p.StartsWith("Game #")));
                        HandHistory history = parser.ParseFullHandHistory(formattedNewString, false);
                        if(history != null)
                        {
                            bool goToShowdown = history.HandActions.Any(p => p.Street == HandHistories.Objects.Cards.Street.Showdown);
                            //We need action for all round and don't have all-in config (we need an another network for that)
                            if (goToShowdown 
                                && history.HandActions.Any(p => p.Street == HandHistories.Objects.Cards.Street.Flop) 
                                && history.HandActions.Any(p => p.Street == HandHistories.Objects.Cards.Street.Turn) 
                                && history.HandActions.Any( p => p.Street == HandHistories.Objects.Cards.Street.River)
                                && !history.HandActions.Any(p => p.IsAllInAction))
                            {
                                if (history.Players.Count( p => p.hasHoleCards == true ) >= 2)
                                {
                                    returnHandHistory.Add(history);
                                }
                            }
                        }
                    }
                });
            };
            return returnHandHistory;
        }

        public void saveMostWinningPlayer(int number)
        {
            /*Thread t = new Thread(() =>
            {
                List<Tuple<int,String>> listPlayer =  PostgresConnector.Instance.getMostWinningPlayer(100);
                using(StreamWriter writer = new StreamWriter("./winningPlayer.dat"))
                {
                    foreach (Tuple<int, String> player in listPlayer)
                    {
                        writer.WriteLine(player.Item1.ToString() + ";" + player.Item2.ToString());
                    }
                    writer.Flush();
                }
            });
            t.Start();*/
        }


        protected virtual void OnRaiseProgressEvent(ProgressEventArgs e)
        {
            if (ProgressEvent != null)
            {
                ProgressEvent(this, e);
            }
        }
    }
}
