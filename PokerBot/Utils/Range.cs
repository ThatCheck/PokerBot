using PokerBot.Entity.Card;
using PokerBot.Hand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Utils
{
    public class Range
    {
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
