using PokerBot.Entity.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Utils
{
    public class NominalValueConverter
    {
        public static String NormalizeFormat(PlayingCardNominalValue card)
        {
            switch (card)
            {
                case PlayingCardNominalValue.Two:
                    return "2";
                case PlayingCardNominalValue.Three:
                    return "3";
                case PlayingCardNominalValue.Four:
                    return "4";
                case PlayingCardNominalValue.Five:
                    return "5";
                case PlayingCardNominalValue.Six:
                    return "6";
                case PlayingCardNominalValue.Seven:
                    return "7";
                case PlayingCardNominalValue.Eight:
                    return "8";
                case PlayingCardNominalValue.Nine:
                    return "9";
                case PlayingCardNominalValue.Ten:
                    return "T";
                case PlayingCardNominalValue.Jack:
                    return "J";
                case PlayingCardNominalValue.Queen:
                    return "Q";
                case PlayingCardNominalValue.King:
                    return "K";
                case PlayingCardNominalValue.Ace:
                    return "A";
                default :
                    throw new ArgumentException("NominalValue unknown");
            }
        }

        public static PlayingCardNominalValue getPlayingCardNominalValue(string card)
        {
            switch (card.ToUpper())
            {
                case "2":
                    return PlayingCardNominalValue.Two;
                case "3":
                    return PlayingCardNominalValue.Three;
                case "4":
                    return PlayingCardNominalValue.Four;
                case "5":
                    return PlayingCardNominalValue.Five;
                case"6":
                    return PlayingCardNominalValue.Six;
                case "7":
                    return PlayingCardNominalValue.Seven;
                case "8":
                    return PlayingCardNominalValue.Eight;
                case "9":
                    return PlayingCardNominalValue.Nine;
                case "T":
                    return PlayingCardNominalValue.Ten;
                case "J":
                    return PlayingCardNominalValue.Jack;
                case "Q":
                    return PlayingCardNominalValue.Queen;
                case "K":
                    return PlayingCardNominalValue.King;
                case "A":
                    return PlayingCardNominalValue.Ace;
                default:
                    throw new ArgumentException("NominalValue unknown");
            }
        }
    }
}
