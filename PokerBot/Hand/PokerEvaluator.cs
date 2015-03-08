using PokerBot.Entity.Card;
using PokerBot.Entity.Hand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Hand
{
    public class PokerEvaluator
    {
        private readonly IPokerHandEvaluator _evaluator;
        private readonly Dictionary<PokerHand, Func<IEnumerable<PlayingCard>, Tuple<bool, IEnumerable<PlayingCard>>>> _hands;

        public PokerEvaluator(IPokerHandEvaluator evaluator)
        {
            _evaluator = evaluator;
            _hands = new Dictionary<PokerHand, Func<IEnumerable<PlayingCard>, Tuple<bool, IEnumerable<PlayingCard>>>>
                         {
                             { PokerHand.HighCard, _evaluator.IsHigh },
                             { PokerHand.Pair, _evaluator.IsPair },
                             { PokerHand.TwoPair, _evaluator.IsTwoPair },
                             { PokerHand.ThreeOfKind, _evaluator.IsThreeOfKind },
                             { PokerHand.Straight, _evaluator.IsStraight },
                             { PokerHand.Flush, _evaluator.IsFlush },
                             { PokerHand.FullHouse, _evaluator.IsFullHouse },
                             { PokerHand.FourOfKind, _evaluator.IsFourOfKind },
                             { PokerHand.StraightFlush, _evaluator.IsStraightFlush },
                             { PokerHand.RoyalFlush, _evaluator.IsRoyalFlush }
                         };
        }

        public PokerHandEvaluationResult EvaluateHand(IEnumerable<PlayingCard> cards)
        {
            var winningHand = _hands.OrderByDescending(hand => hand.Key)
                                        .FirstOrDefault(hand => hand.Value.Invoke(cards).Item1).Key;

            var redundantCall = _hands[winningHand].Invoke(cards);
            var winningCards = redundantCall.Item2;
            var otherCards = cards.Where(card => !winningCards.Contains(card));

            return new PokerHandEvaluationResult(winningHand, winningCards, otherCards);
        }
    }
}
