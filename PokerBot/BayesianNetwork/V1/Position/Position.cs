using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.Position
{
    public class Position : BasePositionPlayer
    {

        public Position(List<HandHistories.Objects.Actions.HandAction> list, String player, HandHistories.Objects.Cards.Street street = HandHistories.Objects.Cards.Street.Preflop)
        {
            int cpt = 0;
            
            foreach(var currHand in list.Where(p => p.Street == street 
                && p.HandActionType != HandHistories.Objects.Actions.HandActionType.POSTS 
                && p.HandActionType != HandHistories.Objects.Actions.HandActionType.BIG_BLIND
                && p.HandActionType != HandHistories.Objects.Actions.HandActionType.SMALL_BLIND))
            {
                if (currHand.PlayerName == player)
                {
                    this.PositionPlayer = (PositionEnumType)cpt;
                    break;
                }
                cpt++;
            }
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
            return new String[] { typeof(Action.Action).Name, typeof(PositionFlop).Name };
        }
    }
}
