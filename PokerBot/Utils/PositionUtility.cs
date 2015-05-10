using HandHistories.Objects.Actions;
using HandHistories.Objects.Hand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Utils
{
    public static class PositionUtility
    {
        public static int getNumberPlayerAtStage(HandHistory hand, HandHistories.Objects.Cards.Street stage)
        {
            var listHand = hand.HandActions;
            if (stage == HandHistories.Objects.Cards.Street.Preflop)
            {
                return listHand.GroupBy(p => p.PlayerName).Count() - listHand.Where(p => p.Street == HandHistories.Objects.Cards.Street.Preflop && (p.HandActionType == HandActionType.FOLD || p.HandActionType == HandActionType.SITTING_OUT || p.HandActionType == HandActionType.DISCONNECTED)).Count();
            }
            else if (stage == HandHistories.Objects.Cards.Street.Flop)
            {
                return listHand.GroupBy(p => p.PlayerName).Count()
                    - listHand.Where(p => p.Street == HandHistories.Objects.Cards.Street.Preflop && (p.HandActionType == HandActionType.FOLD || p.HandActionType == HandActionType.SITTING_OUT || p.HandActionType == HandActionType.DISCONNECTED)).Count()
                    - listHand.Where(p => p.Street == HandHistories.Objects.Cards.Street.Flop && (p.HandActionType == HandActionType.FOLD || p.HandActionType == HandActionType.SITTING_OUT || p.HandActionType == HandActionType.DISCONNECTED)).Count();
            }
            else if (stage == HandHistories.Objects.Cards.Street.Turn)
            {
                return listHand.GroupBy(p => p.PlayerName).Count()
                    - listHand.Where(p => p.Street == HandHistories.Objects.Cards.Street.Preflop && (p.HandActionType == HandActionType.FOLD || p.HandActionType == HandActionType.SITTING_OUT || p.HandActionType == HandActionType.DISCONNECTED)).Count()
                    - listHand.Where(p => p.Street == HandHistories.Objects.Cards.Street.Flop && (p.HandActionType == HandActionType.FOLD || p.HandActionType == HandActionType.SITTING_OUT || p.HandActionType == HandActionType.DISCONNECTED)).Count()
                    - listHand.Where(p => p.Street == HandHistories.Objects.Cards.Street.Turn && (p.HandActionType == HandActionType.FOLD || p.HandActionType == HandActionType.SITTING_OUT || p.HandActionType == HandActionType.DISCONNECTED)).Count();
            }
            else if (stage == HandHistories.Objects.Cards.Street.River)
            {
                return listHand.GroupBy(p => p.PlayerName).Count()
                    - listHand.Where(p => p.Street == HandHistories.Objects.Cards.Street.Preflop && (p.HandActionType == HandActionType.FOLD || p.HandActionType == HandActionType.SITTING_OUT || p.HandActionType == HandActionType.DISCONNECTED)).Count()
                    - listHand.Where(p => p.Street == HandHistories.Objects.Cards.Street.Flop && (p.HandActionType == HandActionType.FOLD || p.HandActionType == HandActionType.SITTING_OUT || p.HandActionType == HandActionType.DISCONNECTED)).Count()
                    - listHand.Where(p => p.Street == HandHistories.Objects.Cards.Street.Turn && (p.HandActionType == HandActionType.FOLD || p.HandActionType == HandActionType.SITTING_OUT || p.HandActionType == HandActionType.DISCONNECTED)).Count()
                    - listHand.Where(p => p.Street == HandHistories.Objects.Cards.Street.River && (p.HandActionType == HandActionType.FOLD || p.HandActionType == HandActionType.SITTING_OUT || p.HandActionType == HandActionType.DISCONNECTED)).Count();
            }
            throw new ArgumentException("Unable to found a correspondance for :" + stage);
        }

        public static int getPosition(HandHistory hand, HandHistories.Objects.Cards.Street stage, string player)
        {
            var listHand = hand.HandActions.Where(p => p.Street == stage &&
                p.HandActionType != HandHistories.Objects.Actions.HandActionType.FOLD
                && p.HandActionType != HandHistories.Objects.Actions.HandActionType.SITTING_OUT && !HandUtility.isPostAction(p)
            );
            Dictionary<String, int> dicoPosition = new Dictionary<string, int>();
            int cpt = 0;
            foreach (var handAction in listHand)
            {
                if (!dicoPosition.ContainsKey(handAction.PlayerName))
                {
                    dicoPosition.Add(handAction.PlayerName, cpt);
                }
                cpt++;
            }

            return dicoPosition[player];
        }

        public static int getNumberPlayerToAct(HandHistory hand, HandHistories.Objects.Cards.Street stage, string player)
        {
           return Math.Max(getNumberPlayerAtStage(hand,stage) - (getPosition(hand,stage,player) + 1),0);
        }

        public static double getRelativePosition(HandHistory hand, HandHistories.Objects.Cards.Street stage, string player)
        {
            return (double)getPosition(hand, stage, player) / (double)getNumberPlayerAtStage(hand, stage);
        }
    }
}
