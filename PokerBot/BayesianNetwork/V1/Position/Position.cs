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
            var listHand = list.Where(p => p.Street == street).OrderByDescending(p => p.ActionNumber);
            int position = 0;
            int cpt = 0;
            String firstPlayerIntercept = null;
            foreach (var hand in listHand)
            {
                if (firstPlayerIntercept == null)
                {
                    firstPlayerIntercept = hand.PlayerName;
                }
                if (firstPlayerIntercept == hand.PlayerName)
                {
                    if (hand.HandActionType == HandHistories.Objects.Actions.HandActionType.FOLD || hand.HandActionType == HandHistories.Objects.Actions.HandActionType.SITTING_OUT)
                    {
                        firstPlayerIntercept = null;
                    }
                    cpt = 0;
                }
                if (hand.PlayerName == player)
                {
                    position = cpt;
                }
                cpt++;
            }

            this.PositionPlayer = (PositionEnumType)position;
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
