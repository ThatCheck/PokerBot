using HandHistories.Objects.Hand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Utils
{
    public static class PotUtility
    {
        public static double getPot(HandHistory hand,HandHistories.Objects.Cards.Street stage = HandHistories.Objects.Cards.Street.Showdown)
        {
            return (double)hand.HandActions.Where(p => p.Street <= stage).Sum(p => Math.Abs(p.Amount));
        }

        public static double potOdds(HandHistory hand, HandHistories.Objects.Cards.Street stage, string player)
        {
            var listHand = hand.HandActions.Where( p => p.Street == stage);
            Dictionary<String, double> moneyDictionnary = getDictionnaryTotalBet(player, listHand);
            double pot = getPot(hand, stage);
            return (moneyDictionnary.Max(p => p.Value) - moneyDictionnary[player]) / pot;
        }

        private static Dictionary<string, double> getDictionnaryTotalBet(string player, IEnumerable<HandHistories.Objects.Actions.HandAction> listHand)
        {
            Dictionary<String, double> moneyDictionnary = new Dictionary<string, double>();
            moneyDictionnary.Add(player, 0);
            foreach (var handAction in listHand)
            {
                if (!moneyDictionnary.ContainsKey(handAction.PlayerName))
                {
                    moneyDictionnary.Add(handAction.PlayerName, 0);
                }
                moneyDictionnary[handAction.PlayerName] += (double)Math.Abs(handAction.Amount);
            }
            return moneyDictionnary;
        }

        public static double betCommited(HandHistory hand, HandHistories.Objects.Cards.Street stage)
        {
            return (double)hand.HandActions.Where(p => p.Street == stage).Sum(p => Math.Abs(p.Amount)) / (double)hand.GameDescription.Limit.BigBlind;
        }

        public static double betToCall(HandHistory hand, HandHistories.Objects.Cards.Street stage, string player)
        {
            var listHand = hand.HandActions.Where( p => p.Street == stage);
            Dictionary<String, double> moneyDictionnary = getDictionnaryTotalBet(player, listHand);
            double max = moneyDictionnary.Max(p => p.Value);
            return (max - moneyDictionnary[player]) / (double)hand.GameDescription.Limit.BigBlind;
        }

        public static double betTotal(HandHistory hand, HandHistories.Objects.Cards.Street stage)
        {
            return (double)hand.HandActions.Sum(p => Math.Abs(p.Amount)) / (double)hand.GameDescription.Limit.BigBlind;
        }
    }
}
