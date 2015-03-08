using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Entity.Card
{
    public class PlayingCard : IEquatable<PlayingCard>, IComparable<PlayingCard>
    {
        public CardSuit Suit { get; private set; }
        public PlayingCardNominalValue NominalValue { get; private set; }


        public PlayingCard(CardSuit suit, PlayingCardNominalValue nominalValue)
        {
            Suit = suit;
            NominalValue = nominalValue;
        }

        public bool Equals(PlayingCard other)
        {
            //Check whether the compared object is null. 
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, other)) return true;

            return Suit.Equals(other.Suit) && NominalValue.Equals(other.NominalValue);
        }

        public override int GetHashCode()
        {
            return Suit.GetHashCode() ^ NominalValue.GetHashCode();
        }


        public int CompareTo(PlayingCard other)
        {
            return this.NominalValue.CompareTo(other.NominalValue);
        }
    }
}
