using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.PotOdds
{
    public class PotOdds : BasePotOdds
    {

        public PotOdds(List<HandHistories.Objects.Actions.HandAction> list, String player, HandHistories.Objects.Cards.Street street = HandHistories.Objects.Cards.Street.Preflop) : base(list,player,street)
        {
            
        }

        public new static string[] getArcForValue()
        {
            return new String[] { typeof(PotOddsFlop).Name, typeof(Action.Action).Name};
        }
    }
}
