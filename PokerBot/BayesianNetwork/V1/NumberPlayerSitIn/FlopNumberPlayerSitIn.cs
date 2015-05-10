using HandHistories.Objects.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.NumberPlayerSitIn
{
    public class FlopNumberPlayerSitIn: BaseNumberPlayerSitIn
    {
        public FlopNumberPlayerSitIn(List<HandAction> listHand)
            : base(listHand.Where(p => p.Street == HandHistories.Objects.Cards.Street.Flop).GroupBy(p => p.PlayerName).Count())
        {

        }

        public new static string getCaseName()
        {
            return System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        }

        public new static string[] getValueName()
        {
            List<String> data = new List<string>();
            NumberPlayerSitInEnumType[] enumArray = new NumberPlayerSitInEnumType[]
            {
                NumberPlayerSitInEnumType.Two,
                NumberPlayerSitInEnumType.Three,
                NumberPlayerSitInEnumType.Four,
                NumberPlayerSitInEnumType.Five,
                NumberPlayerSitInEnumType.Six
            };
            foreach (NumberPlayerSitInEnumType enumValue in enumArray)
            {
                data.Add(enumValue.ToBayesianNetwork());
            }
            return data.ToArray();
        }

        public new static string[] getArcForValue()
        {
            return new String[] { typeof(Action.ActionFlop).Name, typeof(TurnNumberPlayerSitIn).Name };
        }
    }
}
