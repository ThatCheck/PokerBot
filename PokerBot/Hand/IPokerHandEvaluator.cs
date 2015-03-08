using PokerBot.Entity.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Hand
{
    public interface IPokerHandEvaluator
    {
        Tuple<bool, IEnumerable<PlayingCard>> IsHigh(IEnumerable<PlayingCard> cards);
        Tuple<bool, IEnumerable<PlayingCard>> IsPair(IEnumerable<PlayingCard> cards);
        Tuple<bool, IEnumerable<PlayingCard>> IsTwoPair(IEnumerable<PlayingCard> cards);
        Tuple<bool, IEnumerable<PlayingCard>> IsThreeOfKind(IEnumerable<PlayingCard> cards);
        Tuple<bool, IEnumerable<PlayingCard>> IsFourOfKind(IEnumerable<PlayingCard> cards);
        Tuple<bool, IEnumerable<PlayingCard>> IsFlush(IEnumerable<PlayingCard> cards);
        Tuple<bool, IEnumerable<PlayingCard>> IsFullHouse(IEnumerable<PlayingCard> cards);
        Tuple<bool, IEnumerable<PlayingCard>> IsStraight(IEnumerable<PlayingCard> cards);
        Tuple<bool, IEnumerable<PlayingCard>> IsStraightFlush(IEnumerable<PlayingCard> cards);
        Tuple<bool, IEnumerable<PlayingCard>> IsRoyalFlush(IEnumerable<PlayingCard> cards);
    }
}
