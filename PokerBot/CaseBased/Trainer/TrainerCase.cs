using HandHistories.Objects.Actions;
using HandHistories.Objects.Hand;
using NLog;
using PokerBot.BayesianNetwork.V1.HandType;
using PokerBot.CaseBased.Base;
using PokerBot.CaseBased.PostFlop;
using PokerBot.CaseBased.PreFlop;
using PokerBot.Entity.Card;
using PokerBot.Entity.Table;
using PokerBot.Hand;
using PokerBot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerBot.Utils.Extensions;
using PokerBot.BayesianNetwork;
using PokerBot.BayesianNetwork.V1;
namespace PokerBot.CaseBased.Trainer
{
    public class TrainerCase
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public static List<PreflopCase> generatePreFlopCaseForHand(HandHistory handHistory, List<Player> playerList)
        {
            List<PreflopCase> listPFCase = new List<PreflopCase>();
            foreach (Player player in playerList)
            {
                string name = player.Name;
                List<HandAction> iteratorHand = handHistory.HandActions.Copy();
                handHistory.HandActions = new List<HandAction>();
                foreach (var hand in iteratorHand.Where(p => p.Street == HandHistories.Objects.Cards.Street.Preflop))
                {
                    handHistory.HandActions.Add(hand);
                    if (HandUtility.isActionWithAmount(hand) && !HandUtility.isPostAction(hand) && hand.PlayerName == name)
                    {
                        var last = handHistory.HandActions.Last();
                        var street = last.Street;
                        var nameLastActionPlayer = last.PlayerName;
                        PreflopCase pfCase = new PreflopCase();
                        try
                        {
                            pfCase.BetCommitted = PotUtility.betCommited(handHistory, street);
                            pfCase.BetToCall = PotUtility.betToCall(handHistory, street, nameLastActionPlayer);
                            pfCase.HandStrengh = HandUtility.preFlopRank(handHistory.Players[name].HoleCards);
                            pfCase.NumberOfPlayer = handHistory.NumPlayersActive;
                            pfCase.PlayerInCurrentHand = PositionUtility.getNumberPlayerAtStage(handHistory, street);
                            pfCase.PlayerYetToAct = PositionUtility.getNumberPlayerToAct(handHistory, street, name);
                            pfCase.PotOdds = PotUtility.potOdds(handHistory, street, name);
                            pfCase.RelativePosition = PositionUtility.getRelativePosition(handHistory, street, name);
                            pfCase.Card = Tuple.Create<int,int>(TwoPlusTwoHandEvaluator.transformIntoInt(CardConverter.fromIntToPlayingCard(handHistory.Players[nameLastActionPlayer].HoleCards[0])),TwoPlusTwoHandEvaluator.transformIntoInt(CardConverter.fromIntToPlayingCard(handHistory.Players[nameLastActionPlayer].HoleCards[1])));
                            pfCase.CommittedStack = (double)handHistory.HandActions.Where(p => p.PlayerName == nameLastActionPlayer).Sum(p => Math.Abs(p.Amount)) / PotUtility.getPot(handHistory);
                            pfCase.BetPattern = HandUtility.generateHandBetPattern(handHistory.HandActions.Where(p => p.PlayerName == nameLastActionPlayer));
                            pfCase.PlayerStat = player.Stats;
                            listPFCase.Add(pfCase);
                        }
                        catch (Exception ex)
                        {
                            _logger.Error("Unable to parse : " + ex.StackTrace);
                        }
                    }
                }
                handHistory.HandActions = iteratorHand;
            }
            return listPFCase;
        }

        public static List<PostFlopDecisionCase> generatePostFlopDecisionCaseForHand(HandHistory handHistory, Table table, List<Player> playerList)
        {
            List<PostFlopDecisionCase> listPFCase = new List<PostFlopDecisionCase>();
            List<string> listPlayerInHand = handHistory.HandActions.Select(p => p.PlayerName).Distinct().ToList();
            foreach (Player player in playerList)
            {
                string name = player.Name;
                List<HandAction> iteratorHand = handHistory.HandActions.Copy();
                handHistory.HandActions = new List<HandAction>();
                foreach (var hand in iteratorHand.Where(p => p.Street >= HandHistories.Objects.Cards.Street.Preflop && p.Street < HandHistories.Objects.Cards.Street.Showdown))
                {
                    handHistory.HandActions.Add(hand);
                    if (HandUtility.isFoldAction(hand))
                        listPlayerInHand.Remove(hand.PlayerName);
                    if (hand.Street == HandHistories.Objects.Cards.Street.Preflop)
                        continue;
                    if (HandUtility.isActionWithAmount(hand) && !HandUtility.isPostAction(hand) && hand.PlayerName == name)
                    {
                        var last = handHistory.HandActions.Last();
                        var street = last.Street;
                        var nameLastActionPlayer = last.PlayerName;
                        table.HandHistory = handHistory;
                        PostFlopDecisionCase pfCase = new PostFlopDecisionCase();
                        try
                        {
                            pfCase.BetCommitted = PotUtility.betCommited(handHistory, street);
                            pfCase.BetToCall = PotUtility.betToCall(handHistory, street, nameLastActionPlayer);
                            pfCase.BetTotal = PotUtility.betTotal(handHistory, street);
                            pfCase.NumberOfPlayer = handHistory.NumPlayersActive;
                            pfCase.PlayerInCurrentHand = PositionUtility.getNumberPlayerAtStage(handHistory, street);
                            pfCase.PlayerYetToAct = PositionUtility.getNumberPlayerToAct(handHistory, street, name);
                            pfCase.PotOdds = PotUtility.potOdds(handHistory, street, name);
                            pfCase.RelativePosition = PositionUtility.getRelativePosition(handHistory, street, name);
                            pfCase.CommittedStack = (double)handHistory.HandActions.Where(p => p.PlayerName == nameLastActionPlayer).Sum(p => Math.Abs(p.Amount)) / PotUtility.getPot(handHistory);
                            pfCase.BetPattern = HandUtility.generateHandBetPattern(handHistory.HandActions.Where(p => p.PlayerName == nameLastActionPlayer));
                            pfCase.Action = HandUtility.getAction(handHistory.HandActions, hand);

                            Tuple<PlayingCard, PlayingCard> card = Tuple.Create<PlayingCard, PlayingCard>(CardConverter.fromIntToPlayingCard(handHistory.Players[name].HoleCards[0]), CardConverter.fromIntToPlayingCard(handHistory.Players[name].HoleCards[1]));
                            //List<List<Tuple<PlayingCard, PlayingCard>>> listOppRange = new List<List<Tuple<PlayingCard, PlayingCard>>>();
                            IEnumerable<PlayingCard> boardcard = null;
                            if (street == HandHistories.Objects.Cards.Street.Flop)
                            {
                                boardcard = table.Board.getBoardFromFlop().getEvaluator();
                            }
                            else if (street == HandHistories.Objects.Cards.Street.Turn)
                            {
                                boardcard = table.Board.getBoardFromTurn().getEvaluator();
                            }
                            else if (street == HandHistories.Objects.Cards.Street.River)
                            {
                                boardcard = table.Board.getBoardFromRiver().getEvaluator();
                            }
                            /*List<HandTypeEnumType> listHandType = new List<HandTypeEnumType>();
                            foreach (String stringPlayer in listPlayerInHand)
                            {
                                Player selectedPlayer = null;
                                foreach (Player playerTempo in table.ListPlayer)
                                {
                                    if (playerTempo.Name == stringPlayer)
                                    {
                                        selectedPlayer = playerTempo;
                                        break;
                                    }
                                }
                                Network net = new Network();
                                IEnumerable<String> dataResult;
                                if (street == HandHistories.Objects.Cards.Street.Flop)
                                    dataResult = net.getValueForHandType(table, selectedPlayer, HandHistories.Objects.Cards.Street.Flop);
                                else if (street == HandHistories.Objects.Cards.Street.Turn)
                                    dataResult = net.getValueForHandType(table, selectedPlayer, HandHistories.Objects.Cards.Street.Turn);
                                else
                                    dataResult = net.getValueForHandType(table, selectedPlayer);

                                List<HandTypeEnumType> dataResultConvert = new List<HandTypeEnumType>();
                                foreach (var value in dataResult.Take(3))
                                {
                                    listHandType.Add(PokerBot.BayesianNetwork.V1.HandType.ToHandType.toHandType(value));
                                    dataResultConvert.Add(PokerBot.BayesianNetwork.V1.HandType.ToHandType.toHandType(value));
                                }
                                
                                var listRange = Range.getRangeEstimator(dataResultConvert, boardcard, card.ToCollection());
                                if(listRange.Count() > 0)
                                    listOppRange.Add(listRange.ToList());
                            }
                            if (listOppRange.Count() == 0)
                                continue;
                            var ordered = listHandType.OrderByDescending( p => p);
                            pfCase.HandType = Tuple.Create<HandTypeEnumType, HandTypeEnumType, HandTypeEnumType>(ordered.ElementAt(0), ordered.ElementAt(1), ordered.ElementAt(2));*/

                            string ourHand = card.Item1.getStringCard() + " " + card.Item2.getStringCard();
                            string boardHand = "";
                            foreach (PlayingCard board in boardcard)
                            {
                                boardHand += board.getStringCard() + " ";
                            }
                            ulong ourHoldemHand = HoldemHand.Hand.ParseHand(ourHand);
                            ulong boardHoldemHand = HoldemHand.Hand.ParseHand(boardHand);
                            int numberOpp = listPlayerInHand.Count();

                            List<Task> task = new List<Task>();
                            Task<double> handStrengthTask = Task<double>.Factory.StartNew(() => HoldemHand.Hand.HandStrength(ourHoldemHand,boardHoldemHand,numberOpp,0.1));
                            task.Add(handStrengthTask);
                            Task handPotentialTask = null;
                            double ppot = 0;
                            double npot = 0;
                            if(street != HandHistories.Objects.Cards.Street.River)
                            {
                                handPotentialTask = Task.Factory.StartNew(() => HoldemHand.Hand.HandPotential(ourHoldemHand,boardHoldemHand,out ppot, out npot,numberOpp,0.3));
                                task.Add(handPotentialTask);
                            }

                            double totalPot = (double)iteratorHand.Sum(p => Math.Abs(p.Amount));
                            IEnumerable<string> nameWinners = iteratorHand.Where(p => p.HandActionType == HandActionType.WINS || p.HandActionType == HandActionType.WINS_SIDE_POT).Select(p => p.PlayerName);

                            Task<double> qualityTask = Task<double>.Factory.StartNew(() => HoldemHand.Hand.WinOdds(ourHoldemHand,boardHoldemHand,0UL,numberOpp,0.3));
                            task.Add(qualityTask);
                            Task.WaitAll(task.ToArray());

                            pfCase.HandStrengh = handStrengthTask.Result;
                            if(handPotentialTask != null)
                            {
                                pfCase.PositivePotential = ppot;
                                pfCase.NegativePotential = npot;
                            }
                            if (nameWinners.Contains(name))
                            {
                                pfCase.Quality = qualityTask.Result * totalPot;
                            }
                            else
                            {
                                pfCase.Quality = (1 - qualityTask.Result) * -totalPot;
                            }
                            listPFCase.Add(pfCase);
                        }
                        catch (Exception ex)
                        {
                            _logger.Error("Unable to parse : " + ex.StackTrace);
                        }
                    }
                }
                handHistory.HandActions = iteratorHand;
            }
            return listPFCase;
        }

        public static List<PreflopDecisionCase> generatePreFlopDecisionCaseForHand(HandHistory handHistory, List<Player> playerList)
        {
            List<PreflopDecisionCase> listPFCase = new List<PreflopDecisionCase>();
            foreach (Player player in playerList)
            {
                string name = player.Name;
                List<HandAction> iteratorHand = handHistory.HandActions.Copy();
                handHistory.HandActions = new List<HandAction>();
                foreach (var hand in iteratorHand.Where(p => p.Street == HandHistories.Objects.Cards.Street.Preflop))
                {
                    handHistory.HandActions.Add(hand);
                    if (HandUtility.isActionWithAmount(hand) && !HandUtility.isPostAction(hand) && hand.PlayerName == name)
                    {
                        var last = handHistory.HandActions.Last();
                        var street = last.Street;
                        var nameLastActionPlayer = last.PlayerName;
                        PreflopDecisionCase pfCase = new PreflopDecisionCase();
                        try
                        {
                            pfCase.BetCommitted = PotUtility.betCommited(handHistory, street);
                            pfCase.BetToCall = PotUtility.betToCall(handHistory, street, nameLastActionPlayer);
                            pfCase.HandStrengh = HandUtility.preFlopRank(handHistory.Players[name].HoleCards);
                            pfCase.NumberOfPlayer = handHistory.NumPlayersActive;
                            pfCase.PlayerInCurrentHand = PositionUtility.getNumberPlayerAtStage(handHistory, street);
                            pfCase.PlayerYetToAct = PositionUtility.getNumberPlayerToAct(handHistory, street, name);
                            pfCase.PotOdds = PotUtility.potOdds(handHistory, street, name);
                            pfCase.RelativePosition = PositionUtility.getRelativePosition(handHistory, street, name);
                            pfCase.CommittedStack = (double)handHistory.HandActions.Where(p => p.PlayerName == nameLastActionPlayer).Sum(p => Math.Abs(p.Amount)) / PotUtility.getPot(handHistory);
                            pfCase.BetPattern = HandUtility.generateHandBetPattern(handHistory.HandActions.Where(p => p.PlayerName == nameLastActionPlayer));
                            pfCase.Action = HandUtility.getAction(handHistory.HandActions, hand);

                            Tuple<PlayingCard, PlayingCard> card = Tuple.Create<PlayingCard, PlayingCard>(CardConverter.fromIntToPlayingCard(handHistory.Players[name].HoleCards[0]), CardConverter.fromIntToPlayingCard(handHistory.Players[name].HoleCards[1]));
                            List<List<Tuple<PlayingCard, PlayingCard>>> listOppRange = new List<List<Tuple<PlayingCard, PlayingCard>>>();
                            foreach (var playerRange in handHistory.Players)
                            {
                                if (playerRange.hasHoleCards)
                                {
                                    Tuple<PlayingCard, PlayingCard> cardOpp = Tuple.Create<PlayingCard, PlayingCard>(CardConverter.fromIntToPlayingCard(playerRange.HoleCards[0]), CardConverter.fromIntToPlayingCard(playerRange.HoleCards[1]));
                                    listOppRange.Add(new List<Tuple<PlayingCard, PlayingCard>>() { cardOpp });
                                }
                            }
                            double totalPot = (double)iteratorHand.Sum(p => Math.Abs(p.Amount));
                            IEnumerable<string> nameWinners = iteratorHand.Where(p => p.HandActionType == HandActionType.WINS).Select(p => p.PlayerName);
                            if (nameWinners.Contains(name))
                            {
                                pfCase.Quality = HandUtility.getWinOdds(card, new List<PlayingCard>(), listOppRange, 2) * totalPot;
                            }
                            else
                            {
                                pfCase.Quality = (1 - HandUtility.getWinOdds(card, new List<PlayingCard>(), listOppRange, 2)) * -totalPot;
                            }
                            listPFCase.Add(pfCase);
                        }
                        catch (Exception ex)
                        {
                            _logger.Error("Unable to parse : " + ex.StackTrace);
                        }
                    }
                }
                handHistory.HandActions = iteratorHand;
            }
            return listPFCase;
        }
    }
}
