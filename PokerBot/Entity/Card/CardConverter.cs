using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Entity.Card
{
    public static class CardConverter
    {
        private static Dictionary<int, Tuple<CardSuit, PlayingCardNominalValue>> _dictionnaryFromIntToPlayingCard = new Dictionary<int, Tuple<CardSuit, PlayingCardNominalValue>>()
        {
            {0,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Clubs,PlayingCardNominalValue.Two)},
            {1,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Clubs,PlayingCardNominalValue.Three)},
            {2,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Clubs,PlayingCardNominalValue.Four)},
            {3,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Clubs,PlayingCardNominalValue.Five)},
            {4,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Clubs,PlayingCardNominalValue.Six)},
            {5,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Clubs,PlayingCardNominalValue.Seven)},
            {6,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Clubs,PlayingCardNominalValue.Eight)},
            {7,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Clubs,PlayingCardNominalValue.Nine)},
            {8,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Clubs,PlayingCardNominalValue.Ten)},
            {9,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Clubs,PlayingCardNominalValue.Jack)},
            {10,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Clubs,PlayingCardNominalValue.Queen)},
            {11,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Clubs,PlayingCardNominalValue.King)},
            {12,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Clubs,PlayingCardNominalValue.Ace)},
            {13,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Diamonds,PlayingCardNominalValue.Two)},
            {14,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Diamonds,PlayingCardNominalValue.Three)},
            {15,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Diamonds,PlayingCardNominalValue.Four)},
            {16,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Diamonds,PlayingCardNominalValue.Five)},
            {17,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Diamonds,PlayingCardNominalValue.Six)},
            {18,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Diamonds,PlayingCardNominalValue.Seven)},
            {19,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Diamonds,PlayingCardNominalValue.Eight)},
            {20,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Diamonds,PlayingCardNominalValue.Nine)},
            {21,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Diamonds,PlayingCardNominalValue.Ten)},
            {22,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Diamonds,PlayingCardNominalValue.Jack)},
            {23,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Diamonds,PlayingCardNominalValue.Queen)},
            {24,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Diamonds,PlayingCardNominalValue.King)},
            {25,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Diamonds,PlayingCardNominalValue.Ace)},
            {26,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Hearts,PlayingCardNominalValue.Two)},
            {27,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Hearts,PlayingCardNominalValue.Three)},
            {28,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Hearts,PlayingCardNominalValue.Four)},
            {29,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Hearts,PlayingCardNominalValue.Five)},
            {30,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Hearts,PlayingCardNominalValue.Six)},
            {31,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Hearts,PlayingCardNominalValue.Seven)},
            {32,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Hearts,PlayingCardNominalValue.Eight)},
            {33,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Hearts,PlayingCardNominalValue.Nine)},
            {34,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Hearts,PlayingCardNominalValue.Ten)},
            {35,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Hearts,PlayingCardNominalValue.Jack)},
            {36,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Hearts,PlayingCardNominalValue.Queen)},
            {37,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Hearts,PlayingCardNominalValue.King)},
            {38,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Hearts,PlayingCardNominalValue.Ace)},
            {39,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Spades,PlayingCardNominalValue.Two)},
            {40,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Spades,PlayingCardNominalValue.Three)},
            {41,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Spades,PlayingCardNominalValue.Four)},
            {42,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Spades,PlayingCardNominalValue.Five)},
            {43,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Spades,PlayingCardNominalValue.Six)},
            {44,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Spades,PlayingCardNominalValue.Seven)},
            {45,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Spades,PlayingCardNominalValue.Eight)},
            {46,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Spades,PlayingCardNominalValue.Nine)},
            {47,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Spades,PlayingCardNominalValue.Ten)},
            {48,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Spades,PlayingCardNominalValue.Jack)},
            {49,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Spades,PlayingCardNominalValue.Queen)},
            {50,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Spades,PlayingCardNominalValue.King)},
            {51,Tuple.Create<CardSuit,PlayingCardNominalValue>(CardSuit.Spades,PlayingCardNominalValue.Ace)},
        };
        public static PlayingCard fromIntToPlayingCard(HandHistories.Objects.Cards.Card card)
        {
            var result = _dictionnaryFromIntToPlayingCard[card.CardIntValue];
            return new PlayingCard(result.Item1,result.Item2);
        }
    }
}
