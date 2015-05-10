using PokerBot.BayesianNetwork.V1.HandType;
using PokerBot.Entity.Card;
using PokerBot.Hand;
using PokerBot.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Utils
{
    public class Range
    {
        public static HashSet<Tuple<PlayingCard,PlayingCard>> getRangeEstimator(IEnumerable<HandTypeEnumType> handType, IEnumerable<PlayingCard> board, IEnumerable<PlayingCard> notIn)
        {
            HashSet<Tuple<PlayingCard, PlayingCard>> rangeResult = new HashSet<Tuple<PlayingCard, PlayingCard>>();
            List<int> boardvalue = HandUtility.getHandIntValue(board);
            int[] notInRange = HandUtility.getHandIntValue(board).Concat(HandUtility.getHandIntValue(notIn)).ToArray();
            var rangeClearTwoPlusTwo = getAllCombinaisonWithoutCardsSelectedForTwoPlusTwo(notInRange);
            var rangeList = getAllCombinaisonFromTwoPlusTwo(rangeClearTwoPlusTwo);
            foreach (var range in rangeList)
            {
                IEnumerable<PlayingCard> data = board.Concat(range.ToCollection());
                PokerEvaluator pokerEvaluator = new PokerEvaluator(new PokerHandEvaluator());
                PokerHandEvaluationResult result = pokerEvaluator.EvaluateHand(data);
                HandTypeEnumType resultHandType = HandUtility.GetHandTypeEnum(data,result);
                if (handType.Contains(resultHandType))
                {
                    rangeResult.Add(range);
                }
            }
            return rangeResult;
        }

        public static HashSet<PlayingCard> getAllCombinaison()
        {
            HashSet<PlayingCard> returnListCard = new HashSet<PlayingCard>();
            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (PlayingCardNominalValue nomitatedValue in Enum.GetValues(typeof(PlayingCardNominalValue)))
                {
                    returnListCard.Add(new PlayingCard(suit,nomitatedValue));
                }
            }
            return returnListCard;
        }

        public static HashSet<Tuple<PlayingCard,PlayingCard>> getAllCombinaisonFromTwoPlusTwo(HashSet<Tuple<int,int>> toConvert)
        {
            var reversed = TwoPlusTwoHandEvaluator.HandEquivalentList.ToDictionary(kp => kp.Value, kp => kp.Key);
            HashSet<Tuple<PlayingCard,PlayingCard>> newList = new HashSet<Tuple<PlayingCard,PlayingCard>>();
            foreach(var p in toConvert)
            {
                newList.Add(Tuple.Create<PlayingCard,PlayingCard>(new PlayingCard(reversed[p.Item1]),new PlayingCard(reversed[p.Item2])));
            }
            return newList;
        }

        public static HashSet<PlayingCard> getAllCombinaisonWithoutCardsSelected(IEnumerable<PlayingCard> cards)
        {
            return new HashSet<PlayingCard>(getAllCombinaison().Except(cards));
        }


        public static HashSet<Tuple<int,int>> getAllCombinaisonsForTwoPlusTwo()
        {
            HashSet<Tuple<int, int>> data = new HashSet<Tuple<int, int>>();
            var handEquivalentList = TwoPlusTwoHandEvaluator.HandEquivalentList.Values.ToList();
            for(int i = 0; i < 52; i++)
            {
                for (int j = i + 1; j < 52; j++)
                {
                    data.Add(Tuple.Create<int, int>(handEquivalentList.ElementAt(i), handEquivalentList.ElementAt(j)));
                }
            }
            return data;
        }

        public static HashSet<int> getAllCombinaisonOneCardWithoutCardsSelectedForTwoPlusTwo(int[] currentHand)
        {
            var dico = TwoPlusTwoHandEvaluator.HandEquivalentList.Values.ToList();
            dico.RemoveAll(p => currentHand.Contains(p));
            return new HashSet<int>(dico);
        }

        public static HashSet<Tuple<int,int>> getAllCombinaisonWithoutCardsSelectedForTwoPlusTwo(int[] currentHand)
        {
            var dico = getAllCombinaisonsForTwoPlusTwo();
            dico.RemoveWhere(p => currentHand.Contains(p.Item2) || currentHand.Contains(p.Item1));
            return dico;
        }

        public static HashSet<Tuple<int, int, int>> getAllCombinaisonsThreeCardsForTwoPlusTwo()
        {
            HashSet<Tuple<int, int, int>> data = new HashSet<Tuple<int, int, int>>();
            var handEquivalentList = TwoPlusTwoHandEvaluator.HandEquivalentList.Values.ToList();
            for (int i = 0; i < 52; i++)
            {
                for (int j = i + 1; j < 52; j++)
                {
                    for (int k = j + 1; k < 52; k++)
                    {
                        data.Add(Tuple.Create<int, int, int>(handEquivalentList.ElementAt(i), handEquivalentList.ElementAt(j), handEquivalentList.ElementAt(k)));
                    }
                }
            }
            return data;
        }

        public static HashSet<Tuple<int, int, int>> getAllCombinaisonThreeCardsWithoutCardsSelectedForTwoPlusTwo(int[] currentHand)
        {
            var dico = getAllCombinaisonsThreeCardsForTwoPlusTwo();
            dico.RemoveWhere(p => currentHand.Contains(p.Item2) || currentHand.Contains(p.Item1) || currentHand.Contains(p.Item3));
            return dico;
        }
    }
}
