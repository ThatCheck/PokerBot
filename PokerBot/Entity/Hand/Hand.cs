using PokerBot.Entity.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Entity.Hand
{
    public class Hand : IEquatable<Hand>
    {
        public PlayingCard First{get;set;}
        public PlayingCard Second { get; set; }

        public Boolean IsSuited
        {
            get 
            {
                return this.First.Suit == this.Second.Suit;
            }
        }

        public Boolean IsPair
        {
            get 
            {
                return this.First.NominalValue == this.Second.NominalValue;
            }
        }

        public Boolean Contains(PlayingCardNominalValue nominal)
        {
            return this.First.NominalValue == nominal || this.Second.NominalValue == nominal;
        }

        public Hand(Tuple<PlayingCard,PlayingCard> tuple) 
        {
            this.First = tuple.Item1;
            this.Second = tuple.Item2;
        }

        public Hand(PlayingCard first, PlayingCard second)
        {
            this.First = first;
            this.Second = second;
        }

        public IEnumerable<PlayingCard> getEvaluator()
        {
            return new HashSet<PlayingCard>() { this.First, this.Second };
        }

        public string getStringRepresentation()
        {
            return this.First.getStringCard() + " " + this.Second.getStringCard();
        }

        public ulong getMask()
        {
            return HoldemHand.Hand.ParseHand(this.getStringRepresentation());
        }

        public bool Equals(Hand other)
        {
            if (this.First.Equals(other.First) && this.Second.Equals(other.Second))
                return true;
            if (this.First.Equals(other.Second) && this.Second.Equals(other.First))
                return true;
            return false;
        }
    }
}
