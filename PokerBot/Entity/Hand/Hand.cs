using PokerBot.Entity.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Entity.Hand
{
    public class Hand
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

        public Hand(PlayingCard first, PlayingCard second)
        {
            this.First = first;
            this.Second = second;
        }

        public IEnumerable<PlayingCard> getEvaluator()
        {
            return new HashSet<PlayingCard>() { this.First, this.Second };
        }
    }
}
