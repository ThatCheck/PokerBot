using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.PotOdds
{
    public class PotOddsFlop: BasePotOdds
    {
        public PotOddsFlop(List<HandHistories.Objects.Actions.HandAction> list, String player, HandHistories.Objects.Cards.Street street = HandHistories.Objects.Cards.Street.Flop) : base(list,player,street)
        {

        }

        public new static string[] getArcForValue()
        {
            return new String[] { typeof(PotOddsTurn).Name, typeof(Action.ActionFlop).Name };
        }
    }
}
