using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandHistories.Objects.Cards;
using PokerBot.Utils;
using PokerBot.Entity.Card;
using System.Collections.Generic;

namespace PokerBotTest.UtilityTest
{
    [TestClass]
    public class HandUtilityTest
    {
        [TestMethod]
        public void TestPreFlopRank()
        {
            HoleCards holeCards = HoleCards.ForHoldem(new Card("A", "c"), new Card("A", "s"));
            Assert.AreEqual(0, HandUtility.preFlopRank(holeCards));
            holeCards = HoleCards.ForHoldem(new Card("A", "c"), new Card("K", "s"));
            Assert.AreEqual(3, HandUtility.preFlopRank(holeCards));
            holeCards = HoleCards.ForHoldem(new Card("A", "h"), new Card("K", "d"));
            Assert.AreEqual(3, HandUtility.preFlopRank(holeCards));
            holeCards = HoleCards.ForHoldem(new Card("A", "h"), new Card("K", "s"));
            Assert.AreEqual(4, HandUtility.preFlopRank(holeCards));
        }

        [TestMethod]
        public void TestHandStrength()
        {
            PlayingCard ourOne = new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace);
            PlayingCard ourTwo = new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.King);

            PlayingCard boardOne = new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ace);
            PlayingCard boardTwo = new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Jack);
            PlayingCard boardThree = new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Eight);

            PlayingCard opponentOne = new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Two);
            PlayingCard oppenentTwo = new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Three);

            Tuple<PlayingCard, PlayingCard> ourHand = Tuple.Create<PlayingCard, PlayingCard>(ourOne, ourTwo);
            List<PlayingCard> board = new List<PlayingCard>(){boardOne,boardTwo,boardThree};
            List<List<Tuple<PlayingCard, PlayingCard>>> oppList = new List<List<Tuple<PlayingCard, PlayingCard>>>();
            List<Tuple<PlayingCard, PlayingCard>> oppRangeOne = new List<Tuple<PlayingCard, PlayingCard>>();
            oppRangeOne.Add(Tuple.Create<PlayingCard,PlayingCard>(opponentOne,oppenentTwo));
            oppList.Add(oppRangeOne);

            Assert.AreEqual(1,HandUtility.handStrenght(ourHand,board,oppList));

            oppRangeOne.Add(Tuple.Create<PlayingCard, PlayingCard>(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Ace),new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Four)));

            Assert.AreEqual(1, HandUtility.handStrenght(ourHand, board, oppList));

            oppRangeOne.Add(Tuple.Create<PlayingCard, PlayingCard>(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Jack), new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Jack)));

            Assert.AreEqual(0.67, Math.Round(HandUtility.handStrenght(ourHand, board, oppList),2));
        }

        [TestMethod]
        public void TestWinOdds()
        {
            PlayingCard ourOne = new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace);
            PlayingCard ourTwo = new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.King);

            PlayingCard boardOne = new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ace);
            PlayingCard boardTwo = new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Jack);
            PlayingCard boardThree = new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Eight);

            PlayingCard opponentOne = new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Two);
            PlayingCard oppenentTwo = new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Three);

            Tuple<PlayingCard, PlayingCard> ourHand = Tuple.Create<PlayingCard, PlayingCard>(ourOne, ourTwo);
            List<PlayingCard> board = new List<PlayingCard>() { boardOne, boardTwo, boardThree };
            List<List<Tuple<PlayingCard, PlayingCard>>> oppList = new List<List<Tuple<PlayingCard, PlayingCard>>>();
            List<Tuple<PlayingCard, PlayingCard>> oppRangeOne = new List<Tuple<PlayingCard, PlayingCard>>();
            oppRangeOne.Add(Tuple.Create<PlayingCard, PlayingCard>(opponentOne, oppenentTwo));
            oppList.Add(oppRangeOne);

            Assert.AreEqual(0.95, Math.Round(HandUtility.getWinOdds(ourHand, board, oppList,5),2));

            oppRangeOne.Add(Tuple.Create<PlayingCard, PlayingCard>(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Ace), new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Four)));

            Assert.AreEqual(0.90, Math.Round(HandUtility.getWinOdds(ourHand, board, oppList, 5), 2));

            oppRangeOne.Add(Tuple.Create<PlayingCard, PlayingCard>(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Jack), new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Jack)));

            Assert.AreEqual(0.61, Math.Round(HandUtility.getWinOdds(ourHand, board, oppList, 5), 2));

            List<Tuple<PlayingCard, PlayingCard>> oppRangeTwo = new List<Tuple<PlayingCard, PlayingCard>>();
            oppRangeTwo.Add(Tuple.Create<PlayingCard, PlayingCard>(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Queen), new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Ace)));
            oppList.Add(oppRangeTwo);

           Assert.AreEqual(0.51, Math.Round(HandUtility.getWinOdds(ourHand, board, oppList, 5), 2));

            List<Tuple<PlayingCard, PlayingCard>> oppRangeThree = new List<Tuple<PlayingCard, PlayingCard>>();
            oppRangeThree.Add(Tuple.Create<PlayingCard, PlayingCard>(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Five), new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Four)));
            oppList.Add(oppRangeThree);

            Assert.AreEqual(0.50, Math.Round(HandUtility.getWinOdds(ourHand, board, oppList, 5), 2));
        }
    }
}
