using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.HandGroup
{
    public class HandGroup : BaseSmileDefinition
    {
        private Entity.Hand.Hand _hand;
        private HandGroupEnumType _handGroupEnum;
        public HandGroup(Entity.Hand.Hand hand)
        {
            this._hand = hand;
            if ((this._hand.First.NominalValue == Entity.Card.PlayingCardNominalValue.Ace
                || this._hand.First.NominalValue == Entity.Card.PlayingCardNominalValue.King
                || this._hand.First.NominalValue == Entity.Card.PlayingCardNominalValue.Queen
                || this._hand.First.NominalValue == Entity.Card.PlayingCardNominalValue.Jack)
                && this._hand.IsPair)
            {
                this._handGroupEnum = HandGroupEnumType.HandGroup1;
            }
            else if (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.King) && this._hand.IsSuited)
            {
                this._handGroupEnum = HandGroupEnumType.HandGroup1;
            }
            else if ((this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Queen) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Jack) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Jack) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.King) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Queen) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.King))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ten) && this._hand.IsPair)
                )
            {
                this._handGroupEnum = HandGroupEnumType.HandGroup2;
            }
            else if ((this._hand.Contains(Entity.Card.PlayingCardNominalValue.King) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Jack) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ten) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Queen) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Jack) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Jack) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ten) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Queen))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Nine) && this._hand.IsPair)
                )
            {
                this._handGroupEnum = HandGroupEnumType.HandGroup3;
            }
            else if ((this._hand.Contains(Entity.Card.PlayingCardNominalValue.King) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ten) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Queen) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ten) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Jack) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Nine) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ten) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Nine) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Nine) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Eight) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ten) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Nine) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Jack))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.King) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Queen))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Eight) && this._hand.IsPair)
                )
            {
                this._handGroupEnum = HandGroupEnumType.HandGroup4;
            }
            else if ((this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Nine) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Eight) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Seven) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Six) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Five) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Four) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Three) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Two) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Queen) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Nine) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ten) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Eight) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Nine) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Seven) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Eight) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Seven) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Seven) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Six) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.King) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Jack))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Queen) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Jack))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Jack) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ten))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Seven) && this._hand.IsPair)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Six) && this._hand.IsPair)
                )
            {
                this._handGroupEnum = HandGroupEnumType.HandGroup5;
            }
            else if ((this._hand.Contains(Entity.Card.PlayingCardNominalValue.Jack) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Eight) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Eight) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Six) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Seven) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Five) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Six) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Five) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Five) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Four) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ten))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.King) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ten))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Queen) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ten))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Five) && this._hand.IsPair)
                )
            {
                this._handGroupEnum = HandGroupEnumType.HandGroup6;
            }
            else if ((this._hand.Contains(Entity.Card.PlayingCardNominalValue.King) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Nine) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.King) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Eight) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.King) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Seven) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.King) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Six) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.King) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Five) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.King) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Four) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.King) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Three) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.King) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Two) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Six) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Four) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Five) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Three) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Four) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Three) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Eight) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Seven) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Seven) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Six) && this._hand.IsSuited)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Jack) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Nine))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ten) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Nine))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Nine) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Eight))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Four) && this._hand.IsPair)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Three) && this._hand.IsPair)
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Two) && this._hand.IsPair)
                )
            {
                this._handGroupEnum = HandGroupEnumType.HandGroup7;
            }
            else if ((this._hand.Contains(Entity.Card.PlayingCardNominalValue.Jack) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Seven) && this._hand.IsSuited)
                 || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Nine) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Six) && this._hand.IsSuited)
                 || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Eight) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Five) && this._hand.IsSuited)
                 || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Seven) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Four) && this._hand.IsSuited)
                 || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Four) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Two) && this._hand.IsSuited)
                 || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Three) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Two) && this._hand.IsSuited)
                 || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Nine))
                 || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.King) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Nine))
                 || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Queen) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Nine))
                 || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Jack) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Eight))
                 || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ten) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Eight))
                 || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Eight) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Seven))
                 || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Seven) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Six))
                 || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Six) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Five))
                 || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Five) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Four))
                 )
            {
                this._handGroupEnum = HandGroupEnumType.HandGroup8;
            }
            else if ((this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Eight))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Seven))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Six))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Five))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Four))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Three))
                || (this._hand.Contains(Entity.Card.PlayingCardNominalValue.Ace) && this._hand.Contains(Entity.Card.PlayingCardNominalValue.Two))
                )
            {
                this._handGroupEnum = HandGroupEnumType.HandGroup9;
            }
            else
            {
                this._handGroupEnum = HandGroupEnumType.HandGroup10;
            }
        }

        public override String ToString()
        {
           return this._handGroupEnum.ToBayesianNetwork();
        }

        public new static string getCaseName()
        {
            return System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        }

        public new static string[] getValueName()
        {
            List<String> data = new List<string>();
            HandGroupEnumType[] enumArray = new HandGroupEnumType[]
            {
                HandGroupEnumType.HandGroup1,
                HandGroupEnumType.HandGroup2,
                HandGroupEnumType.HandGroup3,
                HandGroupEnumType.HandGroup4,
                HandGroupEnumType.HandGroup5,
                HandGroupEnumType.HandGroup6,
                HandGroupEnumType.HandGroup7,
                HandGroupEnumType.HandGroup8,
                HandGroupEnumType.HandGroup9,
                HandGroupEnumType.HandGroup10
            };
            foreach (HandGroupEnumType enumValue in enumArray)
            {
                data.Add(enumValue.ToBayesianNetwork());
            }
            return data.ToArray();
        }

        public new static string[] getArcForValue()
        {
            return new String[] { typeof(HandType.HandType).Name};
        }
    }
}
