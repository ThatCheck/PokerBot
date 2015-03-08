using PokerBot.Entity.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Comparer
{
    public class PlayingCardNominalValueComparer : IEqualityComparer<PlayingCard>
    {

        #region IEqualityComparer<ThisClass> Members


        public bool Equals(PlayingCard x, PlayingCard y)
        {
            //no null check here, you might want to do that, or correct that to compare just one part of your object
            return x.NominalValue == y.NominalValue;
        }


        public int GetHashCode(PlayingCard obj)
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + obj.NominalValue.GetHashCode();
                return hash;
            }
        }

        #endregion
    }
}
