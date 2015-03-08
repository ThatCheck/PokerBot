using PokerBot.Comparer;
using PokerBot.Entity;
using PokerBot.Entity.Card;
using PokerBot.Entity.Hand;
using PokerBot.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Hand
{
    public class HandOdd
    {
        /*private Tuple<PlayingCard, PlayingCard> _hand;
        private Board _board;

        public HandOdd(Tuple<PlayingCard, PlayingCard> hand, Board board)
        {
            this._hand = hand;
            this._board = board;
        }*/

        public int calculateProbFromFlop(int numberOdd)
        {
            return 1 - (47 - numberOdd) / 47 * (46 - numberOdd) / 46;
        }

        public int calculateProbFromTurn(int numberOdd)
        {
            return  (numberOdd / 46);
        }

        public HashSet<PlayingCard> getOdd(IEnumerable<PlayingCard> hands, IEnumerable<PlayingCard> cards)
        {
            ConcurrentBag<PlayingCard> returnOdd = new ConcurrentBag<PlayingCard>();
            var handToEvaluate = cards.Concat(hands);
            HashSet<PlayingCard> allCards = Range.getAllCombinaisonWithoutCardsSelected(handToEvaluate);

            PokerHandEvaluator pokerHandEvaluator = new PokerHandEvaluator();
            PokerEvaluator evaluator = new PokerEvaluator(new PokerHandEvaluator());
            //First determine the best combinaison actually in hand
            PokerHandEvaluationResult result = evaluator.EvaluateHand(handToEvaluate);

            //Now we need to find cards which ameliorate our hands
            Parallel.ForEach(allCards, card =>
            {
                HashSet<PlayingCard> newCards = new HashSet<PlayingCard>(handToEvaluate);
                newCards.Add(card);
                PokerHandEvaluationResult resultToNewHand = evaluator.EvaluateHand(newCards);

                HashSet<PlayingCard> newBoards = new HashSet<PlayingCard>(cards);
                newBoards.Add(card);

                //We have no interest to add HighCard to Out because it's better to the opponents ! 
                if (resultToNewHand.betterThan(result) && resultToNewHand.Result != PokerHand.HighCard && !result.ResultCards.SequenceEqual(resultToNewHand.ResultCards))
                {
                    bool candAdd = false;
                    if (resultToNewHand.Result == PokerHand.Pair)
                    {
                        var intersect = newCards.Intersect(hands, new PlayingCardNominalValueComparer());
                        if (intersect.Any())
                        {
                            // If I get the highest pair, then it's a out
                            candAdd = hands.Any( hand => hand.NominalValue.Equals(newBoards.OrderByDescending(value => value.NominalValue).First().NominalValue))
                                && !resultToNewHand.ResultCards.SequenceEqual(result.ResultCards);
                        }
                    }
                    else if (resultToNewHand.Result == PokerHand.TwoPair)
                    {
                        var intersect = newCards.Intersect(hands, new PlayingCardNominalValueComparer());
                        if (intersect.Any())
                        {
                            // Same logic than pair, if I have the highest pair, it's ok
                            candAdd = intersect.OrderByDescending(value => value.NominalValue).First().NominalValue == newBoards.OrderByDescending(value => value.NominalValue).First().NominalValue 
                                && newBoards.Intersect(hands, new PlayingCardNominalValueComparer()).Count() == 2
                                && !resultToNewHand.ResultCards.SequenceEqual(result.ResultCards);
                        }
                    }
                    else
                    {
                        candAdd = true;
                    }
                    /*else if (resultToNewHand.Result == PokerHand.ThreeOfKind)
                    {
                        var intersect = newCards.Intersect(hands, new PlayingCardNominalValueComparer());
                        candAdd = intersect.Any();
                    }
                    else if (resultToNewHand.Result == PokerHand.FourOfKind)
                    {
                        var intersect = newCards.Intersect(hands, new PlayingCardNominalValueComparer());
                        candAdd = intersect.Any();
                    }*/
                    if (candAdd)
                    {
                        returnOdd.Add(card);
                    }
                }
            });
            return new HashSet<PlayingCard>(returnOdd);
        }
    }
}
