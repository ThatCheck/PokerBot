using HandHistories.Objects.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.NumberPlayerSitIn
{
    public class NumberPlayerSitIn : BaseNumberPlayerSitIn
    {
        public NumberPlayerSitIn(List<HandAction> listHand) : base(listHand.GroupBy(p => p.PlayerName).Count() - listHand.Where( p => p.Street == HandHistories.Objects.Cards.Street.Preflop && p.HandActionType == HandActionType.FOLD).Count())
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
            return new String[] { typeof(Action.Action).Name, typeof(FlopNumberPlayerSitIn).Name };
        }
    }
}
