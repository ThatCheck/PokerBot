using PokerBot.CaseBased.Base;
using PokerBot.Entity.Table;
using PokerBot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.CaseBased.PreFlop
{
    [Serializable]
    public class PreflopDecisionCase : DecisionCase
    {
        private double _handStrengh;

        public double HandStrengh
        {
            get { return _handStrengh; }
            set { _handStrengh = value; }
        }

        #region weight
        public const int NUMBER_PLAYER_WEIGHT = 5;
        public const int NUMBER_PLAYER_IN_HAND_WEIGHT = 10;
        public const int NUMBER_PLAYER_YET_TO_ACT_WEIGHT = 5;
        public const int RELATIVE_POSITION_WEIGHT = 10;
        public const int BET_COMMITED_WEIGHT = 10;
        public const int BET_TO_CALL_WEIGHT = 10;
        public const int POT_ODDS_WEIGHT = 5;
        public const int COMMITTED_STACK_WEIGHT = 5;
        public const int HAND_RANK_WEIGHT = 50;
        #endregion

        public override double similarityTo(CaseBase pfCaseTemp)
        {
            PreflopDecisionCase pfCase = (PreflopDecisionCase)((DecisionCase)pfCaseTemp);
            double numberPlayer = Distance.EuclidianDistance(this.NumberOfPlayer, pfCase.NumberOfPlayer, 6) * NUMBER_PLAYER_WEIGHT;
            double numberPlayerInHand = Distance.EuclidianDistance(this.PlayerInCurrentHand, pfCase.PlayerInCurrentHand, 6) * NUMBER_PLAYER_IN_HAND_WEIGHT;
            double numberPlayerYetAct = Distance.EuclidianDistance(this.PlayerYetToAct, pfCase.PlayerYetToAct, 5) * NUMBER_PLAYER_YET_TO_ACT_WEIGHT;
            double relativePosition = Distance.EuclidianDistance(this.RelativePosition, pfCase.RelativePosition, 1) * RELATIVE_POSITION_WEIGHT;
            double betCommitted = Distance.ExponentialDecay(this.BetCommitted, pfCase.BetCommitted, 200, 4) * BET_COMMITED_WEIGHT;
            double betToCall = Distance.ExponentialDecay(this.BetToCall, pfCase.BetToCall, 200, 2) * BET_TO_CALL_WEIGHT;
            double potsOdds = Distance.EuclidianDistance(this.PotOdds, pfCase.PotOdds, 100) * POT_ODDS_WEIGHT;
            double committedStack = Distance.EuclidianDistance(this.CommittedStack, pfCase.CommittedStack, 100) * COMMITTED_STACK_WEIGHT;
            double handRank = Distance.ExponentialDecay(this.HandStrengh, pfCase.HandStrengh, 169, 3) * BET_TO_CALL_WEIGHT;
            return (numberPlayer + numberPlayerInHand + numberPlayerYetAct + relativePosition + betCommitted + betToCall + potsOdds + committedStack + handRank) / (double)(NUMBER_PLAYER_WEIGHT + NUMBER_PLAYER_IN_HAND_WEIGHT + NUMBER_PLAYER_YET_TO_ACT_WEIGHT + RELATIVE_POSITION_WEIGHT + BET_COMMITED_WEIGHT + BET_TO_CALL_WEIGHT + POT_ODDS_WEIGHT + COMMITTED_STACK_WEIGHT + HAND_RANK_WEIGHT);
        }

        public PreflopCase toPreflopCase(Player player)
        {
            PreflopCase pfCase = new PreflopCase();
                pfCase.BetCommitted = this.BetCommitted;
                pfCase.BetPattern = this.BetPattern;
                pfCase.BetToCall = this.BetToCall;
                pfCase.CommittedStack = this.CommittedStack;
                pfCase.HandStrengh = this.HandStrengh;
                pfCase.NumberOfPlayer = this.NumberOfPlayer;
                pfCase.PlayerInCurrentHand = this.PlayerInCurrentHand;
                pfCase.PlayerStat = player.Stats;
                pfCase.PlayerYetToAct = this.PlayerYetToAct;
                pfCase.PotOdds = this.PotOdds;
                pfCase.RelativePosition = this.RelativePosition;
                return pfCase;
        }


    }
}
