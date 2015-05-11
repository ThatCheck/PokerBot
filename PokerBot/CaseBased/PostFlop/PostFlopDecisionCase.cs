using PokerBot.BayesianNetwork.V1.HandType;
using PokerBot.CaseBased.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.CaseBased.PostFlop
{
    [Serializable]
    public class PostFlopDecisionCase : DecisionCase
    {
        private double _handStrengh;
        private Tuple<HandType, HandType, HandType> _handType;
        private double _positivePotential;
        private double _negativePotential;
        private double _betTotal;

        public double PositivePotential
        {
            get { return _positivePotential; }
            set { _positivePotential = value; }
        }

        public double NegativePotential
        {
            get { return _negativePotential; }
            set { _negativePotential = value; }
        } 

        public Tuple<HandType, HandType, HandType> HandType
        {
            get { return _handType; }
            set { _handType = value; }
        }

        public double HandStrengh
        {
            get { return _handStrengh; }
            set { _handStrengh = value; }
        }

        public double BetTotal
        {
            get { return _betTotal; }
            set { _betTotal = value; }
        }


        #region weight
        public const int NUMBER_PLAYER_WEIGHT = 5;
        public const int NUMBER_PLAYER_IN_HAND_WEIGHT = 10;
        public const int NUMBER_PLAYER_YET_TO_ACT_WEIGHT = 5;
        public const int RELATIVE_POSITION_WEIGHT = 10;
        public const int BET_COMMITED_WEIGHT = 20;
        public const int BET_TO_CALL_WEIGHT = 20;
        public const int POT_ODDS_WEIGHT = 5;
        public const int COMMITTED_STACK_WEIGHT = 5;
        public const int HAND_RANK_WEIGHT = 50;
        #endregion


    }
}
