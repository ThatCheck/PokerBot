using HandHistories.Objects.Hand;
using PokerBot.CaseBased.Base;
using PokerBot.Entity.Card;
using PokerBot.Entity.Enum;
using PokerBot.Entity.Player;
using PokerBot.Hand;
using PokerBot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.CaseBased.PreFlop
{
    [Serializable]
    public class PreflopCase : CaseBase
    {
        private double _handStrengh;
        private Tuple<int, int> _card;
        private Stats _playerStat;

        #region weight
        public const int NUMBER_PLAYER_WEIGHT = 5;
        public const int NUMBER_PLAYER_IN_HAND_WEIGHT = 10;
        public const int NUMBER_PLAYER_YET_TO_ACT_WEIGHT = 5;
        public const int RELATIVE_POSITION_WEIGHT = 10;
        public const int BET_COMMITED_WEIGHT = 10;
        public const int BET_TO_CALL_WEIGHT = 10;
        public const int POT_ODDS_WEIGHT = 5;
        public const int COMMITTED_STACK_WEIGHT = 5;
        public const int PLAYER_STAT_WEIGHT = 10;
        #endregion


        public Stats PlayerStat
        {
            get { return _playerStat; }
            set { _playerStat = value; }
        }

        public double HandStrengh
        {
            get { return _handStrengh; }
            set { _handStrengh = value; }
        }

        public Tuple<int, int> Card
        {
            get { return _card; }
            set { _card = value; }
        }

        public PreflopCase()
        {

        }

        public override double similarityTo(CaseBase pfCaseTemp)
        {
            PreflopCase pfCase = (PreflopCase)pfCaseTemp;
            double numberPlayer = Distance.EuclidianDistance(this.NumberOfPlayer, pfCase.NumberOfPlayer, 6) * NUMBER_PLAYER_WEIGHT;
            double numberPlayerInHand = Distance.EuclidianDistance(this.PlayerInCurrentHand, pfCase.PlayerInCurrentHand, 6) * NUMBER_PLAYER_IN_HAND_WEIGHT;
            double numberPlayerYetAct = Distance.EuclidianDistance(this.PlayerYetToAct, pfCase.PlayerYetToAct, 5) * NUMBER_PLAYER_YET_TO_ACT_WEIGHT;
            double relativePosition = Distance.EuclidianDistance(this.RelativePosition, pfCase.RelativePosition, 1) * RELATIVE_POSITION_WEIGHT;
            double betCommitted = Distance.ExponentialDecay(this.BetCommitted, pfCase.BetCommitted, 200,4) * BET_COMMITED_WEIGHT;
            double betToCall = Distance.ExponentialDecay(this.BetToCall, pfCase.BetToCall, 200,2) * BET_TO_CALL_WEIGHT;
            double potsOdds = Distance.EuclidianDistance(this.PotOdds, pfCase.PotOdds, 100) * POT_ODDS_WEIGHT;
            double committedStack = Distance.EuclidianDistance(this.CommittedStack, pfCase.CommittedStack, 100) * COMMITTED_STACK_WEIGHT;

            double stats = (Distance.ExponentialDecay((double)this.PlayerStat.Vpip, (double)pfCase.PlayerStat.Vpip, 100, 2)
                + Distance.ExponentialDecay((double)this.PlayerStat.Pfr, (double)pfCase.PlayerStat.Pfr, 100, 2)
                + Distance.ExponentialDecay((double)this.PlayerStat.ThreeBetPF, (double)pfCase.PlayerStat.ThreeBetPF, 100, 2)
                + Distance.ExponentialDecay((double)this.PlayerStat.FoldToThreeBet, (double)pfCase.PlayerStat.FoldToThreeBet, 100, 2)) / 4.0D;
            stats *= PLAYER_STAT_WEIGHT;

            return (numberPlayer + numberPlayerInHand + numberPlayerYetAct + relativePosition + betCommitted + betToCall + potsOdds + committedStack + stats) / (double)(NUMBER_PLAYER_WEIGHT + NUMBER_PLAYER_IN_HAND_WEIGHT + NUMBER_PLAYER_YET_TO_ACT_WEIGHT + RELATIVE_POSITION_WEIGHT + BET_COMMITED_WEIGHT + BET_TO_CALL_WEIGHT + POT_ODDS_WEIGHT + COMMITTED_STACK_WEIGHT + PLAYER_STAT_WEIGHT);
        }

        public static IEnumerable<Tuple<PlayingCard, PlayingCard>> getRangeFromList(List<PreflopCase> listPf)
        {
            List<Tuple<PlayingCard, PlayingCard>> listReturnCard = new List<Tuple<PlayingCard, PlayingCard>>();
            foreach (PreflopCase pfCase in listPf)
            {
                var tuple = Tuple.Create<PlayingCard, PlayingCard>(HandUtility.getPlayingCardFromTwoPlusTwo(pfCase.Card.Item1), HandUtility.getPlayingCardFromTwoPlusTwo(pfCase.Card.Item2));
            }
            return listReturnCard.Distinct();
        }
    }
}
