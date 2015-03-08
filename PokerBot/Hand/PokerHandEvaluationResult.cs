using PokerBot.Entity.Card;
using PokerBot.Entity.Hand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Hand
{
    public class PokerHandEvaluationResult : IComparable<PokerHandEvaluationResult>
    {
        public PokerHand Result { get; private set; }
        //Get the value of cards
        public IEnumerable<PlayingCard> ResultCards { get; private set; }
        //Get The highest cards 
        public IEnumerable<PlayingCard> HighCards { get; private set; }

        public PokerHandEvaluationResult(PokerHand result, IEnumerable<PlayingCard> resultCards, IEnumerable<PlayingCard> highCards)
        {
            Result = result;
            ResultCards = resultCards;
            HighCards = highCards;
        }

        /**
         * If return -1, instance is worst then other
         * If return 0, instance is equal to other
         * If return 1, instance is better than other
         */
        public int CompareTo(PokerHandEvaluationResult other)
        {
            int result;
            if (Result == other.Result)
            {
                //Special case for straight
                var instanceCompare = ResultCards;
                var otherInstanceCompare = other.ResultCards;
                if ((Result == PokerHand.Straight && other.Result == PokerHand.Straight) || (Result == PokerHand.StraightFlush && other.Result == PokerHand.StraightFlush))
                {
                    if (ResultCards.Any(group => group.NominalValue == PlayingCardNominalValue.Ace) && ResultCards.Any(group => group.NominalValue == PlayingCardNominalValue.Five))
                    {
                        instanceCompare = instanceCompare.Where(group => group.NominalValue != PlayingCardNominalValue.Ace);
                    }
                     if (other.ResultCards.Any(group => group.NominalValue == PlayingCardNominalValue.Ace) && other.ResultCards.Any(group => group.NominalValue == PlayingCardNominalValue.Five))
                    {
                        otherInstanceCompare = otherInstanceCompare.Where(group => group.NominalValue != PlayingCardNominalValue.Ace);
                    }
                }
                if (Result == PokerHand.Flush && other.Result == PokerHand.Flush)
                {
                    //We need to compare five cards
                    instanceCompare = instanceCompare.OrderByDescending(cards => cards.NominalValue);
                    otherInstanceCompare = otherInstanceCompare.OrderByDescending(cards => cards.NominalValue);
                    int cpt = 0;
                    do
                    {
                        result = instanceCompare.ElementAt(cpt).CompareTo(otherInstanceCompare.ElementAt(cpt));
                        cpt++;
                    } while (cpt < instanceCompare.Count() && cpt < otherInstanceCompare.Count() && result == 0);
                }
                else if (Result == PokerHand.FullHouse && other.Result == PokerHand.FullHouse)
                {
                    var instanceCompareGroup = instanceCompare.OrderByDescending(cards => cards.NominalValue);
                    var otherInstanceCompareGroup = otherInstanceCompare.OrderByDescending(cards => cards.NominalValue);
                    result = instanceCompareGroup.ElementAt(0).CompareTo(otherInstanceCompareGroup.ElementAt(0));
                    if (result == 0)
                    {
                        result = instanceCompareGroup.ElementAt(2).CompareTo(otherInstanceCompareGroup.ElementAt(2));
                    }
                }
                else
                {
                    result = ResultCards == null ? 0 : instanceCompare.Max().CompareTo(otherInstanceCompare.Max());
                }
                if (result == 0) result = HighCards.Count() == 0 ? 0 : HighCards.Max().CompareTo(other.HighCards.Max());
            }
            else
            {
                result = Result.CompareTo(other.Result);
            }
            return result;
        }

        public bool betterThan(PokerHandEvaluationResult other)
        {
            return this.CompareTo(other) == 1;
        }

        public bool equalsTo(PokerHandEvaluationResult other)
        {
            return this.CompareTo(other) == 0;
        }

        public bool worseThan(PokerHandEvaluationResult other)
        {
            return this.CompareTo(other) == -1;
        }
    }
}
