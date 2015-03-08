using PokerBot.Entity.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Hand
{
    public class PokerHandEvaluator : IPokerHandEvaluator
    {
        private IEnumerable<IGrouping<PlayingCardNominalValue, PlayingCard>> GroupByNominalValue(IEnumerable<PlayingCard> cards)
        {
            return cards.GroupBy(card => card.NominalValue);
        }

        private IEnumerable<IGrouping<PlayingCardNominalValue, PlayingCard>> Pairs(IEnumerable<PlayingCard> cards)
        {
            return GroupByNominalValue(cards).Where(group => group.Count() == 2);
        }

        private IEnumerable<IGrouping<PlayingCardNominalValue, PlayingCard>> Triplets(IEnumerable<PlayingCard> cards)
        {
            return GroupByNominalValue(cards).Where(group => group.Count() == 3);
        }

        private IEnumerable<IGrouping<CardSuit, PlayingCard>> Suits(IEnumerable<PlayingCard> cards)
        {
            return cards.GroupBy(card => card.Suit);
        }

        private bool isAllSameSuits(IEnumerable<PlayingCard> cards)
        {
            CardSuit reference = cards.First().Suit;
            foreach(PlayingCard card in cards)
            {
                if (card.Suit != reference)
                    return false;
            }
            return true;
        }

        public Tuple<bool, IEnumerable<PlayingCard>> IsHigh(IEnumerable<PlayingCard> cards)
        {
            var values = GroupByNominalValue(cards);
            var winningCards = values.OrderByDescending(value => value.Key);

            return new Tuple<bool, IEnumerable<PlayingCard>>(true, winningCards.First());
        }

        public Tuple<bool, IEnumerable<PlayingCard>> IsPair(IEnumerable<PlayingCard> cards)
        {
            if (cards.Count() < 2)
            {
                return new Tuple<bool, IEnumerable<PlayingCard>>(false, null);
            }
            var pairs = Pairs(cards);

            var result = pairs.Count() == 1 && !Triplets(cards).Any();
            var winningCards = result ? pairs.SelectMany(group => group.Select(card => card)) : null;

            return new Tuple<bool, IEnumerable<PlayingCard>>(result, winningCards);
        }

        public Tuple<bool, IEnumerable<PlayingCard>> IsTwoPair(IEnumerable<PlayingCard> cards)
        {
            if (cards.Count() < 4)
            {
                return new Tuple<bool, IEnumerable<PlayingCard>>(false, null);
            }
            var pairs = Pairs(cards);

            var result = pairs.Count() == 2;
            var winningCards = result ? pairs.SelectMany(group => group.Select(card => card)) : null;

            return new Tuple<bool, IEnumerable<PlayingCard>>(result, winningCards);
        }

        public Tuple<bool, IEnumerable<PlayingCard>> IsThreeOfKind(IEnumerable<PlayingCard> cards)
        {
            if (cards.Count() < 3)
            {
                return new Tuple<bool, IEnumerable<PlayingCard>>(false, null);
            }
            var triplets = Triplets(cards);

            var result = triplets.Count() == 1 && !Pairs(cards).Any();
            var winningCards = result ? triplets.SelectMany(group => group.Select(card => card)) : null;

            return new Tuple<bool, IEnumerable<PlayingCard>>(result, winningCards);
        }

        public Tuple<bool, IEnumerable<PlayingCard>> IsFourOfKind(IEnumerable<PlayingCard> cards)
        {
            if (cards.Count() < 4)
            {
                return new Tuple<bool, IEnumerable<PlayingCard>>(false, null);
            }
            var values = GroupByNominalValue(cards);

            var result = values.Any(group => group.Count() >= 4);
            var winningCards = result ? values.Where(group => group.Count() >= 4).SelectMany(group => group.Select(card => card)) : null;

            return new Tuple<bool, IEnumerable<PlayingCard>>(result, winningCards);
        }

        public Tuple<bool, IEnumerable<PlayingCard>> IsFlush(IEnumerable<PlayingCard> cards)
        {
            if (cards.Count() < 5)
            {
                return new Tuple<bool, IEnumerable<PlayingCard>>(false, null);
            }
            var winningSuit = Suits(cards).Where(suit => suit.Count() >= 5);

            var result = winningSuit.Any();
            var winningCards = result ? winningSuit.SelectMany(group => group.Select(card => card)) : null;

            return new Tuple<bool, IEnumerable<PlayingCard>>(result, winningCards);
        }

        public Tuple<bool, IEnumerable<PlayingCard>> IsFullHouse(IEnumerable<PlayingCard> cards)
        {
            if (cards.Count() < 5)
            {
                return new Tuple<bool, IEnumerable<PlayingCard>>(false, null);
            }
            var pairs = Pairs(cards);
            var triplets = Triplets(cards);

            var result = pairs.Any() && triplets.Any();
            if (!result) return new Tuple<bool, IEnumerable<PlayingCard>>(false, null);
            var winningPair = pairs.OrderByDescending(group => group.Key).FirstOrDefault().Select(card => card);
            var winningTriplet = triplets.OrderByDescending(group => group.Key).FirstOrDefault().Select(card => card);

            var winningCards = winningPair.Concat(winningTriplet);
            return new Tuple<bool, IEnumerable<PlayingCard>>(true, winningCards);
        }

        public Tuple<bool, IEnumerable<PlayingCard>> IsStraight(IEnumerable<PlayingCard> cards)
        {
            if (cards.Count() < 5)
            {
                return new Tuple<bool, IEnumerable<PlayingCard>>(false, null);
            }
            var distinctValues = GroupByNominalValue(cards);
            var isStraightNoAce = IsStraightNoAce(distinctValues);

            if (isStraightNoAce.Item1)
            {
                return new Tuple<bool, IEnumerable<PlayingCard>>(true, isStraightNoAce.Item2);
            }

            var isStraightWithAce = IsStraightWithAce(distinctValues);
            if (isStraightWithAce.Item1.Item1)
            {
                return new Tuple<bool, IEnumerable<PlayingCard>>(true, isStraightWithAce.Item1.Item2);
            }

            return new Tuple<bool, IEnumerable<PlayingCard>>(false, null);
        }

        private enum AceKind
        {
            AceLow,
            AceHigh
        }

        private Tuple<Tuple<bool, IEnumerable<PlayingCard>>, AceKind?> IsStraightWithAce(IEnumerable<IGrouping<PlayingCardNominalValue, PlayingCard>> distinctValues)
        {
            // if has no ace, don't bother
            if (!distinctValues.Any(group => group.Key == PlayingCardNominalValue.Ace)) return new Tuple<Tuple<bool, IEnumerable<PlayingCard>>, AceKind?>(new Tuple<bool, IEnumerable<PlayingCard>>(false, null), null);

            var ace = distinctValues.Where(group => group.Key == PlayingCardNominalValue.Ace).Select(group => group.First()).First();
            var aceRemoved = distinctValues.Where(group => group.Key != PlayingCardNominalValue.Ace);

            var ascending = aceRemoved.OrderBy(value => value.Key).Take(4);
            if (ascending.Max(group => group.Key) == PlayingCardNominalValue.Five)
            {
                var winningCards = ascending.Select(group => group.First()).Concat(new List<PlayingCard> { ace });
                var result = new Tuple<bool, IEnumerable<PlayingCard>>(true, winningCards);
                return new Tuple<Tuple<bool, IEnumerable<PlayingCard>>, AceKind?>(result, AceKind.AceLow);
            }

            var descending = aceRemoved.OrderByDescending(value => value.Key).Take(4);
            if (descending.Min(group => group.Key) == PlayingCardNominalValue.Ten)
            {
                var winningCards = descending.Select(group => group.First());
                var result = new Tuple<bool, IEnumerable<PlayingCard>>(true, winningCards);
                return new Tuple<Tuple<bool, IEnumerable<PlayingCard>>, AceKind?>(result, AceKind.AceHigh);
            }

            return new Tuple<Tuple<bool, IEnumerable<PlayingCard>>, AceKind?>(new Tuple<bool, IEnumerable<PlayingCard>>(false, null), null);
        }

        private Tuple<Tuple<bool, IEnumerable<PlayingCard>>, AceKind?> IsStraightWithAce(IEnumerable<PlayingCard> cards)
        {
            return IsStraightWithAce(GroupByNominalValue(cards));
        }

        private Tuple<bool, IEnumerable<PlayingCard>> IsStraightNoAce(IEnumerable<IGrouping<PlayingCardNominalValue, PlayingCard>> distinctValues)
        {
            // if has ace, don't bother
            if (distinctValues.Any(group => group.Key == PlayingCardNominalValue.Ace)) return new Tuple<bool, IEnumerable<PlayingCard>>(false, null);

            var sortedValues = distinctValues.OrderBy(group => (int)group.Key);
            var possibleLows = sortedValues.Take(Math.Abs(5 - (sortedValues.Count() + 1)));

            var skippedCards = 0;
            foreach (var possibleLow in possibleLows)
            {
                var theCards = sortedValues.Skip(skippedCards).Take(5).Select(group => group.First());
                var isStraight = theCards.Max(card => card.NominalValue) == possibleLow.Key + 4;

                if (isStraight)
                {
                    return new Tuple<bool, IEnumerable<PlayingCard>>(true, theCards);
                }
                skippedCards++;
            }

            return new Tuple<bool, IEnumerable<PlayingCard>>(false, null);
        }

        public Tuple<bool, IEnumerable<PlayingCard>> IsStraightFlush(IEnumerable<PlayingCard> cards)
        {
            if (cards.Count() < 5)
            {
                return new Tuple<bool, IEnumerable<PlayingCard>>(false, null);
            }
            var isFlush = IsFlush(cards);
            if (!isFlush.Item1) return new Tuple<bool, IEnumerable<PlayingCard>>(false, null);

            var isStraight = IsStraight(cards);
            if (!isStraight.Item1) return new Tuple<bool, IEnumerable<PlayingCard>>(false, null);

            return new Tuple<bool, IEnumerable<PlayingCard>>(true, isStraight.Item2);
        }

        public Tuple<bool, IEnumerable<PlayingCard>> IsRoyalFlush(IEnumerable<PlayingCard> cards)
        {
            if (cards.Count() < 5)
            {
                return new Tuple<bool, IEnumerable<PlayingCard>>(false,null);
            }
            var straightWithAceResult = IsStraightWithAce(cards);
            var result = IsFlush(cards).Item1 && straightWithAceResult.Item1.Item1 && straightWithAceResult.Item2 == AceKind.AceHigh;
            return new Tuple<bool, IEnumerable<PlayingCard>>(result, straightWithAceResult.Item1.Item2);
        }

        public bool PossibilityBackDoorFlush(IEnumerable<PlayingCard> cards)
        {
            var winningSuit = Suits(cards).Where(suit => suit.Count() == 3);
            var result = winningSuit.Any();
            if (result)
            {
                return true;
            }
            
            return false;
        }

        public bool PossibilityFlush(IEnumerable<PlayingCard> cards)
        {
            var winningSuit = Suits(cards).Where(suit => suit.Count() == 4);
            var result = winningSuit.Any();
            if (result)
            {
                return true;
            }

            return false;
        }

        public bool PossibilityThreeOfKind(IEnumerable<PlayingCard> cards)
        {
            var pairs = Pairs(cards);
            var result = pairs.Count() >= 1;

            if (result)
            {
                return true;
            }
            
            return false;
        }

        public bool PossibilityFourOfKind(IEnumerable<PlayingCard> cards)
        {
            var triplets = Triplets(cards);

            var result = triplets.Count() == 1;

            if (result)
            {
                return true;
            }
            
            return false;
        }

        public bool PossibilityTwoPairs(IEnumerable<PlayingCard> cards)
        {
            var pairs = Pairs(cards);
            var result = pairs.Count() == 1;

            if (result)
            {
                return true;
            }

            return false;
        }

        public bool PossibilityFullHouse(IEnumerable<PlayingCard> cards)
        {
            var pairs = Pairs(cards);
            var result = pairs.Count() == 2;

            if (result)
            {
                return true;
            }

            var triplets = Triplets(cards);

            result = triplets.Count() == 1;

            if (result)
            {
                return true;
            }

            return false;
        }

        public bool PossibilityOfInsideStraigh(IEnumerable<PlayingCard> cards)
        {
            var distinctValues = GroupByNominalValue(cards);
            //var ace = distinctValues.Where(group => group.Key == PlayingCardNominalValue.Ace).Select(group => group.First()).First();
            var aceRemoved = distinctValues.Where(group => group.Key != PlayingCardNominalValue.Ace);

            var ascending = aceRemoved.OrderBy(value => value.Key).Take(4);
            if (ascending.Max(group => group.Key) == PlayingCardNominalValue.Four)
            {
                return true;
            }

            var descending = aceRemoved.OrderByDescending(value => value.Key).Take(4);
            if (descending.Min(group => group.Key) == PlayingCardNominalValue.Jack)
            {
                return true;
            }

            ascending = aceRemoved.OrderBy(value => value.Key);
            for (int skip = 0; skip < aceRemoved.Count() - 4; skip++)
            {
                var firstTwo = aceRemoved.OrderBy(value => value.Key).Skip(skip).Take(2);
                // Skip index + 3 because we already have two card and we need the two last
                var lastTwo = aceRemoved.OrderBy(value => value.Key).Skip(skip+3).Take(2);
                if (firstTwo.Last().Key + 2 == lastTwo.First().Key)
                    return true;
            }

            return false;
        }

        public bool PossibilityOfBilateralStraigh(IEnumerable<PlayingCard> cards)
        {
            var distinctValues = GroupByNominalValue(cards);

            var aceRemoved = distinctValues.Where(group => group.Key != PlayingCardNominalValue.Ace);
            var sortedValues = aceRemoved.OrderBy(group => (int)group.Key);
            var possibleLows = sortedValues.Take(4 - sortedValues.Count() + 1);

            var skippedCards = 0;
            foreach (var possibleLow in possibleLows)
            {
                var theCards = sortedValues.Skip(skippedCards).Take(4).Select(group => group.First());
                var isStraight = theCards.Max(card => card.NominalValue) == possibleLow.Key + 3;

                if (isStraight)
                {
                    return true;
                }
                skippedCards++;
            }
            return false;
        }

        public bool PossibilityOfFlushBilateralStraigh(IEnumerable<PlayingCard> cards)
        {
            var distinctValues = GroupByNominalValue(cards);

            var aceRemoved = distinctValues.Where(group => group.Key != PlayingCardNominalValue.Ace);
            var sortedValues = aceRemoved.OrderBy(group => (int)group.Key);
            var possibleLows = sortedValues.Take(4 - sortedValues.Count() + 1);

            var skippedCards = 0;
            foreach (var possibleLow in possibleLows)
            {
                var theCards = sortedValues.Skip(skippedCards).Take(4).Select(group => group.First());
                var isStraight = theCards.Max(card => card.NominalValue) == possibleLow.Key + 3 && isAllSameSuits(theCards);

                if (isStraight)
                {
                    return true;
                }
                skippedCards++;
            }
            return false;
        }

        public bool PossibilityOfFlushInsideStraigh(IEnumerable<PlayingCard> cards)
        {
            var distinctValues = GroupByNominalValue(cards);
            //var ace = distinctValues.Where(group => group.Key == PlayingCardNominalValue.Ace).Select(group => group.First()).First();
            var aceRemoved = distinctValues.Where(group => group.Key != PlayingCardNominalValue.Ace);

            var ascending = aceRemoved.OrderBy(value => value.Key).Take(4);
            if (ascending.Max(group => group.Key) == PlayingCardNominalValue.Four && isAllSameSuits(ascending.Select(group => group.First())))
            {
                return true;
            }

            var descending = aceRemoved.OrderByDescending(value => value.Key).Take(4);
            if (descending.Min(group => group.Key) == PlayingCardNominalValue.Jack && isAllSameSuits(descending.Select(group => group.First())))
            {
                return true;
            }

            ascending = aceRemoved.OrderBy(value => value.Key);
            for (int skip = 0; skip < aceRemoved.Count() - 4; skip++)
            {
                var firstTwo = aceRemoved.OrderBy(value => value.Key).Skip(skip).Take(2);
                // Skip index + 3 because we already have two card and we need the two last
                var lastTwo = aceRemoved.OrderBy(value => value.Key).Skip(skip + 3).Take(2);
                if (firstTwo.Last().Key + 2 == lastTwo.First().Key && isAllSameSuits(firstTwo.Concat(lastTwo).Select(group => group.First())))
                    return true;
            }

            return false;
        }
    }
}
