using PokerBot.Entity.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Utils
{
    public class CardSuitConverter
    {
        public static String NormalizeFormat(CardSuit card)
        {
            switch(card){
                case CardSuit.Clubs:
                    return "c";
                case CardSuit.Diamonds:
                    return "d";
                case CardSuit.Hearts:
                    return "h";
                case CardSuit.Spades:
                    return "s";
                default:
                    throw new ArgumentException("Suit unknown");
            }
        }

        public static CardSuit getCardSuit(string card)
        {
            switch (card)
            {
                case "c":
                    return CardSuit.Clubs;
                case "d":
                    return CardSuit.Diamonds;
                case "h":
                    return CardSuit.Hearts;
                case "s":
                    return CardSuit.Spades;
                default:
                    throw new ArgumentException("Suit unknown");
            }
        }
    }
}
