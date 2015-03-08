using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.Position
{
    public class PositionRiver: Position
    {
        public PositionRiver(List<HandHistories.Objects.Actions.HandAction> list, String player)
            : base(list, player, HandHistories.Objects.Cards.Street.River) 
        {

        }

        public new static string getCaseName()
        {
            return System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        }

        public new static string[] getValueName()
        {
            List<String> data = new List<string>();
            PositionEnumType[] enumArray = new PositionEnumType[]
            {
               PositionEnumType.First,
               PositionEnumType.Second,
               PositionEnumType.Third,
               PositionEnumType.Fourth,
               PositionEnumType.Fifth,
               PositionEnumType.Sixth
            };
            foreach (PositionEnumType enumValue in enumArray)
            {
                data.Add(enumValue.ToBayesianNetwork());
            }
            return data.ToArray();
        }

        public new static string[] getArcForValue()
        {
            return new String[] { typeof(Action.ActionRiver).Name};
        }
    }
}
