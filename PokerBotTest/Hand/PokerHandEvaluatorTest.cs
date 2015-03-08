using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerBot.Hand;
using PokerBot.Entity.Card;
using System.Collections.Generic;
using PokerBot.Entity.Hand;

namespace PokerBotTest.Hand
{
    [TestClass]
    public class PokerHandEvaluatorTest
    {
        private PokerEvaluator _evaluator;

        public PokerHandEvaluatorTest()
        {
            this._evaluator = new PokerEvaluator(new PokerHandEvaluator());
        }

        [TestMethod]
        public void TestVersusCard()
        {
            //HighCards vs HighCard
            HashSet<PlayingCard> cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Jack));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));

            PokerHandEvaluationResult result = this._evaluator.EvaluateHand(cards);

            HashSet<PlayingCard> against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Jack));
            against.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.King));

            
            PokerHandEvaluationResult againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result),true);
            Assert.AreEqual(againstResult.betterThan(result), false);
            Assert.AreEqual(againstResult.equalsTo(result), false);

            //HighCards vs better HighCard
            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Ace));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), false);
            Assert.AreEqual(againstResult.betterThan(result), true);
            Assert.AreEqual(againstResult.equalsTo(result), false);

            //HighCard vs Pair
            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.King));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), false);
            Assert.AreEqual(againstResult.betterThan(result), true);
            Assert.AreEqual(againstResult.equalsTo(result), false);

            //Pair vs better Pair
            cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Ace));

            result = this._evaluator.EvaluateHand(cards);

            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.King));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), true);
            Assert.AreEqual(againstResult.betterThan(result), false);
            Assert.AreEqual(againstResult.equalsTo(result), false);

            //Pair vs equal Pair
            cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Ace));

            result = this._evaluator.EvaluateHand(cards);

            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            against.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Ace));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), false);
            Assert.AreEqual(againstResult.betterThan(result), false);
            Assert.AreEqual(againstResult.equalsTo(result), true);

            //Pair vs ThreeOfKind
            cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Ace));

            result = this._evaluator.EvaluateHand(cards);

            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.King));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), false);
            Assert.AreEqual(againstResult.betterThan(result), true);
            Assert.AreEqual(againstResult.equalsTo(result), false);

            //ThreeOfKind vs ThreeOfKind better
            cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Ace));

            result = this._evaluator.EvaluateHand(cards);

            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.King));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), true);
            Assert.AreEqual(againstResult.betterThan(result), false);
            Assert.AreEqual(againstResult.equalsTo(result), false);

            //ThreeOfKind vs ThreeOfKind equals
            cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Ace));

            result = this._evaluator.EvaluateHand(cards);

            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            against.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Ace));
            against.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Ace));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), false);
            Assert.AreEqual(againstResult.betterThan(result), false);
            Assert.AreEqual(againstResult.equalsTo(result), true);

            //FourOfKind vs ThreeOfKind
            cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ace));

            result = this._evaluator.EvaluateHand(cards);

            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            against.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Ace));
            against.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Ace));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), true);
            Assert.AreEqual(againstResult.betterThan(result), false);
            Assert.AreEqual(againstResult.equalsTo(result), false);

            //FourOfKind vs better FourOfKind
            cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ace));

            result = this._evaluator.EvaluateHand(cards);

            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.King));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), true);
            Assert.AreEqual(againstResult.betterThan(result), false);
            Assert.AreEqual(againstResult.equalsTo(result), false);

            //Straight vs better Straight
            cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Two));
            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Three));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Four));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Five));

            result = this._evaluator.EvaluateHand(cards);

            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Six));
            against.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Two));
            against.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Three));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Four));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Five));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), false);
            Assert.AreEqual(againstResult.betterThan(result), true);
            Assert.AreEqual(againstResult.equalsTo(result), false);

            //Straight vs better Straight
            cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Two));
            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Three));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Four));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Five));

            result = this._evaluator.EvaluateHand(cards);

            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            against.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Queen));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Jack));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ten));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), false);
            Assert.AreEqual(againstResult.betterThan(result), true);
            Assert.AreEqual(againstResult.equalsTo(result), false);

            //Flush vs better Flush
            cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Two));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ten));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Four));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Five));

            result = this._evaluator.EvaluateHand(cards);

            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ace));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Seven));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Queen));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Jack));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ten));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), false);
            Assert.AreEqual(againstResult.betterThan(result), true);
            Assert.AreEqual(againstResult.equalsTo(result), false);

            //Flush vs straight
            cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Two));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ten));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Four));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Five));

            result = this._evaluator.EvaluateHand(cards);

            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            against.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Queen));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Jack));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ten));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), true);
            Assert.AreEqual(againstResult.betterThan(result), false);
            Assert.AreEqual(againstResult.equalsTo(result), false);

            //Flush vs straight Flush
            cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Two));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ten));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Four));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Five));

            result = this._evaluator.EvaluateHand(cards);

            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Nine));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Queen));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Jack));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ten));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), false);
            Assert.AreEqual(againstResult.betterThan(result), true);
            Assert.AreEqual(againstResult.equalsTo(result), false);

            //Straihg vs straight Flush
            cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Two));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ten));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Four));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Five));

            result = this._evaluator.EvaluateHand(cards);

            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Nine));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Queen));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Jack));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ten));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), false);
            Assert.AreEqual(againstResult.betterThan(result), true);
            Assert.AreEqual(againstResult.equalsTo(result), false);


            //straight Flush vs straight Flush better
            cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Two));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Three));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Four));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Five));

            result = this._evaluator.EvaluateHand(cards);

            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Nine));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Queen));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Jack));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ten));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), false);
            Assert.AreEqual(againstResult.betterThan(result), true);
            Assert.AreEqual(againstResult.equalsTo(result), false);

            //straight Flush vs straight royal Flush
            cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Two));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Three));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Four));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Five));

            result = this._evaluator.EvaluateHand(cards);

            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ace));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Queen));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Jack));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ten));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), false);
            Assert.AreEqual(againstResult.betterThan(result), true);
            Assert.AreEqual(againstResult.equalsTo(result), false);

            //Full vs better
            cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.King));
            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.King));

            result = this._evaluator.EvaluateHand(cards);

            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ace));
            against.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Ace));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), true);
            Assert.AreEqual(againstResult.betterThan(result), false);
            Assert.AreEqual(againstResult.equalsTo(result), false);

            cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.King));
            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.King));

            result = this._evaluator.EvaluateHand(cards);

            against = new HashSet<PlayingCard>();
            against.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Two));
            against.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Two));
            against.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Two));
            against.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.King));
            against.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.King));

            againstResult = this._evaluator.EvaluateHand(against);

            Assert.AreEqual(againstResult.worseThan(result), true);
            Assert.AreEqual(againstResult.betterThan(result), false);
            Assert.AreEqual(againstResult.equalsTo(result), false);
        }

        [TestMethod]
        public void TestHighCard()
        {
            HashSet<PlayingCard> cards = new HashSet<PlayingCard>();
            cards.Add(new PlayingCard(CardSuit.Clubs,PlayingCardNominalValue.Jack));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));

            PokerHandEvaluationResult result = this._evaluator.EvaluateHand(cards);
            Assert.AreEqual(result.Result, PokerHand.HighCard);
        }

        [TestMethod]
        public void TestPairCard()
        {
            HashSet<PlayingCard> cards = new HashSet<PlayingCard>();

            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Jack));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Jack));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Four));

            PokerHandEvaluationResult result = this._evaluator.EvaluateHand(cards);
            Assert.AreEqual(result.Result, PokerHand.Pair);
        }

        [TestMethod]
        public void TestTwoPairCard()
        {
            HashSet<PlayingCard> cards = new HashSet<PlayingCard>();

            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Jack));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Jack));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Ace));

            PokerHandEvaluationResult result = this._evaluator.EvaluateHand(cards);
            Assert.AreEqual(result.Result, PokerHand.TwoPair);
        }

        [TestMethod]
        public void TestFlushCard()
        {
            HashSet<PlayingCard> cards = new HashSet<PlayingCard>();

            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Jack));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Five));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Six));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Seven));

            PokerHandEvaluationResult result = this._evaluator.EvaluateHand(cards);
            Assert.AreEqual(result.Result, PokerHand.Flush);
        }

        [TestMethod]
        public void TestStraightCard()
        {
            HashSet<PlayingCard> cards = new HashSet<PlayingCard>();

            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Two));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Three));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Four));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Five));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Six));

            PokerHandEvaluationResult result = this._evaluator.EvaluateHand(cards);
            Assert.AreEqual(result.Result, PokerHand.Straight,"This is not a straight");

            cards = new HashSet<PlayingCard>();

            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Two));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Three));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Four));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Five));

            result = this._evaluator.EvaluateHand(cards);
            Assert.AreEqual(result.Result, PokerHand.Straight);

            cards = new HashSet<PlayingCard>();

            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Jack));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Nine));
            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Queen));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Eight));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Four));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ten));

            result = this._evaluator.EvaluateHand(cards);
            Assert.AreEqual(result.Result, PokerHand.Straight);

            cards = new HashSet<PlayingCard>();

            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Jack));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Queen));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.King));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Four));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ten));

            result = this._evaluator.EvaluateHand(cards);
            Assert.AreEqual(result.Result, PokerHand.Straight);
        }

        [TestMethod]
        public void TestThreeOfKindCard()
        {
            HashSet<PlayingCard> cards = new HashSet<PlayingCard>();

            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Five));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Five));
            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Five));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Six));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Seven));

            PokerHandEvaluationResult result = this._evaluator.EvaluateHand(cards);
            Assert.AreEqual(result.Result, PokerHand.ThreeOfKind);
            Assert.AreNotEqual(result.Result, PokerHand.Pair);
        }

        [TestMethod]
        public void TestFourOfKindCard()
        {
            HashSet<PlayingCard> cards = new HashSet<PlayingCard>();

            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Five));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Five));
            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Five));
            cards.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Five));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Seven));

            PokerHandEvaluationResult result = this._evaluator.EvaluateHand(cards);
            Assert.AreEqual(result.Result, PokerHand.FourOfKind);
            Assert.AreNotEqual(result.Result, PokerHand.ThreeOfKind);
            Assert.AreNotEqual(result.Result, PokerHand.Pair);
        }

        public void TestStraightFlushCard()
        {
            HashSet<PlayingCard> cards = new HashSet<PlayingCard>();

            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Two));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Three));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Four));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Five));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Six));

            PokerHandEvaluationResult result = this._evaluator.EvaluateHand(cards);
            Assert.AreEqual(result.Result, PokerHand.StraightFlush);

            cards = new HashSet<PlayingCard>();

            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Two));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Three));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Four));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Five));

            result = this._evaluator.EvaluateHand(cards);
            Assert.AreNotEqual(result.Result, PokerHand.Flush);
        }

        [TestMethod]
        public void TestFullHouseCard()
        {
            HashSet<PlayingCard> cards = new HashSet<PlayingCard>();

            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Five));
            cards.Add(new PlayingCard(CardSuit.Diamonds, PlayingCardNominalValue.Five));
            cards.Add(new PlayingCard(CardSuit.Hearts, PlayingCardNominalValue.Five));
            cards.Add(new PlayingCard(CardSuit.Spades, PlayingCardNominalValue.Seven));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Seven));

            PokerHandEvaluationResult result = this._evaluator.EvaluateHand(cards);
            Assert.AreEqual(result.Result, PokerHand.FullHouse);
            Assert.AreNotEqual(result.Result, PokerHand.ThreeOfKind);
            Assert.AreNotEqual(result.Result, PokerHand.Pair);
        }
        [TestMethod]
        public void TestRoyalFlushCard()
        {
            HashSet<PlayingCard> cards = new HashSet<PlayingCard>();

            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ace));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.King));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Queen));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Jack));
            cards.Add(new PlayingCard(CardSuit.Clubs, PlayingCardNominalValue.Ten));

            PokerHandEvaluationResult result = this._evaluator.EvaluateHand(cards);
            Assert.AreEqual(result.Result, PokerHand.RoyalFlush);
            Assert.AreNotEqual(result.Result, PokerHand.Straight);
            Assert.AreNotEqual(result.Result, PokerHand.StraightFlush);
            Assert.AreNotEqual(result.Result, PokerHand.Flush);
        }
    }
}
