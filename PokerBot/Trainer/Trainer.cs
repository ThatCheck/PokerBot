using HandHistories.Objects.GameDescription;
using HandHistories.Objects.Hand;
using HandHistories.Parser.Parsers.Base;
using HandHistories.Parser.Parsers.Factory;
using NLog;
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
                    String[] data = (String[])addingClass.GetMethod("getValueName", BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static).Invoke(null, null);
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

        public void generateDataTraining(String[] inputHand, String output,SiteName site)
        {

            _logger.Info("Start Training !");
            _logger.Info("Generate header ! ");
            Object locker = new Object();
            Object lockerIntIncrement = new Object();
            using (StreamWriter stream = new StreamWriter(output))
            {
                stream.WriteLine(string.Join(" ",BayesianNetwork.V1.Network.getHeader()));
                foreach (string pathInput in inputHand)
                {
                    _logger.Info("Training ON => " + pathInput);
                    IEnumerable<HandHistory> listHand = this.endcodeHandFromFile(pathInput, site);
                    this._numberEstimatedTrainingHand = 0;
                    foreach(var hand in listHand)
                    {
                        foreach (var player in hand.Players)
                        {
                            if (player.hasHoleCards)
                            {
                                this._numberEstimatedTrainingHand++;
                            }
                        }
                    }
                    _logger.Info("Extract " + listHand.Count() + "hand(s)");
                    int numberCurrentSend = 0;
                    ParallelOptions po = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = Environment.ProcessorCount
                    };
                    Parallel.ForEach(listHand, po,hand =>
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
                                    BayesianNetwork.V1.Network net = new BayesianNetwork.V1.Network();
                                    Player selectedPlayer = table.ListPlayer.Where(p => p.Name == player.PlayerName).First();
                                    if (selectedPlayer == null)
                                    {
                                        throw new InvalidDataException("Unable to load player");
                                    }
                                    net.setTableForTraining(table, selectedPlayer);
                                    watch.Stop();
                                    _logger.Info("[TIME : " + watch.ElapsedMilliseconds +" ] : Training ok for player " + player.PlayerName + " and ID HAND " + hand.HandId);
                                    lock(locker)
                                    {
                                        stream.WriteLine(string.Join(" ", net.getValue()));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.Warn("Unable to train network for data : " + hand.FullHandHistoryText +"\n ERROR : " + ex.Message + "\n Trace : "+ ex.StackTrace );
                                }
                                finally
                                {
                                    Interlocked.Increment(ref numberCurrentSend);
                                    this.OnRaiseProgressEvent(new ProgressEventArgs(numberCurrentSend, _numberEstimatedTrainingHand));
                                }
                            }
                        }
                    });
                }
            }
        }

        public IEnumerable<HandHistories.Objects.Hand.HandHistory> endcodeHandFromFile(String path, SiteName sitename)
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
                        formattedNewString = formattedNewString.Remove(formattedNewString.LastIndexOf(Environment.NewLine));
                        formattedNewString = formattedNewString.Remove(formattedNewString.LastIndexOf(Environment.NewLine));
                        formattedNewString = formattedNewString.Remove(formattedNewString.LastIndexOf(Environment.NewLine));
                        formattedNewString = formattedNewString.Remove(formattedNewString.LastIndexOf(Environment.NewLine));
                        HandHistory history = parser.ParseFullHandHistory(formattedNewString, true);
                        bool goToShowdown = false;
                        foreach (var handAction in history.HandActions)
                        {
                            if (handAction.Street == HandHistories.Objects.Cards.Street.Showdown)
                            {
                                goToShowdown = true;
                            }
                        }
                        //We need action for all round and don't have all-in config (we need an another network for that)
                        if (goToShowdown 
                            && history.HandActions.Any(p => p.Street == HandHistories.Objects.Cards.Street.Flop) 
                            && history.HandActions.Any(p => p.Street == HandHistories.Objects.Cards.Street.Turn) 
                            && history.HandActions.Any( p => p.Street == HandHistories.Objects.Cards.Street.River)
                            && !history.HandActions.Any(p => p.IsAllInAction))
                        {
                            int number = 0;
                            foreach (HandHistories.Objects.Players.Player player in history.Players)
                            {
                                if (player.hasHoleCards)
                                {
                                    number++;
                                }
                            }
                            if (number >= 2)
                            {
                                returnHandHistory.Add(history);
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
