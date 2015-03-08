using HandHistories.Objects.Hand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.CaseBased.Evaluator
{
    public class ProfitEvaluator : ICaseEvaluator
    {
        public double getQuality(HandHistory hands, string PlayerName)
        {
            foreach (var hand in hands.HandActions)
            {
                if(PlayerName.Equals(hand.PlayerName))
                {
                    if (hand.IsWinningsAction)
                    {
                        return (double)Math.Abs(hand.Amount);
                    }
                    if (hand.HandActionType == HandHistories.Objects.Actions.HandActionType.FOLD)
                    {
                        return (double) -hands.HandActions.Where(p => p.PlayerName.Equals(PlayerName)).Sum(p => Math.Abs(p.Amount));
                    }
                }
            }
            return 0;
        }
    }
}
