using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerBot.Hand;
using System.Collections.Generic;
using PokerBot.Entity.Card;

namespace PokerBotTest.Hand
{
    [TestClass]
    public class HandOddTest
    {
        [TestMethod]
        public void CalculOdd()
        {
            HandOdd odd = new HandOdd();

            HashSet<PlayingCard> hands = new HashSet<PlayingCard>();
            hands.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.King));
            hands.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Nine));

            HashSet<PlayingCard> hashset = new HashSet<PlayingCard>();
            hashset.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Ace));
            hashset.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Two));

            hashset.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Ten));

            HashSet<PlayingCard> oddResult = odd.getOdd(hands,hashset);
            Assert.AreEqual(oddResult.Count, 9);

            hands = new HashSet<PlayingCard>();
            hands.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Two));
            hands.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Two));

            hashset = new HashSet<PlayingCard>();
            hashset.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Queen));
            hashset.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Four));
            hashset.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Nine));

            oddResult = odd.getOdd(hands, hashset);
            Assert.AreEqual(oddResult.Count, 2);

            hands = new HashSet<PlayingCard>();
            hands.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Ace));
            hands.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Eight));

            hashset = new HashSet<PlayingCard>();
            hashset.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Jack));
            hashset.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Five));
            hashset.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Two));

            oddResult = odd.getOdd(hands, hashset);
            Assert.AreEqual(oddResult.Count, 3);

            hands = new HashSet<PlayingCard>();
            hands.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Jack));
            hands.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Nine));

            hashset = new HashSet<PlayingCard>();
            hashset.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Queen));
            hashset.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Eight));
            hashset.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Four));

            oddResult = odd.getOdd(hands, hashset);
            Assert.AreEqual(oddResult.Count, 4);

            hands = new HashSet<PlayingCard>();
            hands.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.King));
            hands.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Queen));

            hashset = new HashSet<PlayingCard>();
            hashset.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.King));
            hashset.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Queen));
            hashset.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Five));

            oddResult = odd.getOdd(hands, hashset);
            Assert.AreEqual(oddResult.Count, 4);

            hands = new HashSet<PlayingCard>();
            hands.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            hands.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Queen));

            hashset = new HashSet<PlayingCard>();
            hashset.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ace));
            hashset.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ten));
            hashset.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Three));

            oddResult = odd.getOdd(hands, hashset);
            Assert.AreEqual(oddResult.Count, 5);

            hands = new HashSet<PlayingCard>();
            hands.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Nine));
            hands.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Seven));

            hashset = new HashSet<PlayingCard>();
            hashset.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Two));
            hashset.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Three));
            hashset.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Jack));

            oddResult = odd.getOdd(hands, hashset);
            Assert.AreEqual(oddResult.Count, 0);

            hands = new HashSet<PlayingCard>();
            hands.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ace));
            hands.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Jack));

            hashset = new HashSet<PlayingCard>();
            hashset.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ten));
            hashset.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Eight));
            hashset.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Two));

            oddResult = odd.getOdd(hands, hashset);
            Assert.AreEqual(oddResult.Count, 6);

            hands = new HashSet<PlayingCard>();
            hands.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Six));
            hands.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Six));

            hashset = new HashSet<PlayingCard>();
            hashset.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Six));
            hashset.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Seven));
            hashset.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Jack));

            oddResult = odd.getOdd(hands, hashset);
            Assert.AreEqual(oddResult.Count, 7);

            hands = new HashSet<PlayingCard>();
            hands.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Nine));
            hands.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Eight));

            hashset = new HashSet<PlayingCard>();
            hashset.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Seven));
            hashset.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Ten));
            hashset.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Three));

            oddResult = odd.getOdd(hands, hashset);
            Assert.AreEqual(oddResult.Count, 8);

            hands = new HashSet<PlayingCard>();
            hands.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.King));
            hands.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Jack));

            hashset = new HashSet<PlayingCard>();
            hashset.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Ace));
            hashset.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Six));
            hashset.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Eight));

            oddResult = odd.getOdd(hands, hashset);
            Assert.AreEqual(oddResult.Count, 9);

            hands = new HashSet<PlayingCard>();
            hands.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            hands.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.King));

            hashset = new HashSet<PlayingCard>();
            hashset.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Queen));
            hashset.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ten));
            hashset.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Two));

            oddResult = odd.getOdd(hands, hashset);
            Assert.AreEqual(oddResult.Count, 10);

            hands = new HashSet<PlayingCard>();
            hands.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ace));
            hands.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.King));

            hashset = new HashSet<PlayingCard>();
            hashset.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Jack));
            hashset.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Queen));
            hashset.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Three));

            oddResult = odd.getOdd(hands, hashset);
            Assert.AreEqual(oddResult.Count, 18);

            hands = new HashSet<PlayingCard>();
            hands.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.King));
            hands.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Queen));

            hashset = new HashSet<PlayingCard>();
            hashset.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Ten));
            hashset.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Jack));
            hashset.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Four));

            oddResult = odd.getOdd(hands, hashset);
            Assert.AreEqual(oddResult.Count, 21);
        }
    }
}
