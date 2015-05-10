using HandHistories.Objects.Actions;
using HandHistories.Objects.Hand;
using NLog;
using PokerBot.CaseBased.Base;
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

                            Tuple<PlayingCard,PlayingCard> card = Tuple.Create<PlayingCard,PlayingCard>(CardConverter.fromIntToPlayingCard(handHistory.Players[name].HoleCards[0]),CardConverter.fromIntToPlayingCard(handHistory.Players[name].HoleCards[1]));
                            List<List<Tuple<PlayingCard,PlayingCard>>> listOppRange = new List<List<Tuple<PlayingCard,PlayingCard>>>();
                            foreach(var playerRange in handHistory.Players)
                            {
                                if(playerRange.hasHoleCards)
                                {
                                    Tuple<PlayingCard,PlayingCard> cardOpp = Tuple.Create<PlayingCard,PlayingCard>(CardConverter.fromIntToPlayingCard(playerRange.HoleCards[0]),CardConverter.fromIntToPlayingCard(playerRange.HoleCards[1]));
                                    listOppRange.Add(new List<Tuple<PlayingCard,PlayingCard>>(){cardOpp});
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
                                pfCase.Quality = (1 - HandUtility.getWinOdds(card, new List<PlayingCard>(), listOppRange, 2)) * - totalPot;
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
