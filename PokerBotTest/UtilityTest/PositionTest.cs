﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandHistories.Parser.Parsers.Factory;
using HandHistories.Parser.Parsers.Base;
using HandHistories.Objects.Hand;
using PokerBot.Utils;

namespace PokerBotTest.UtilityTest
{
    [TestClass]
    public class PositionTest
    {
        [TestMethod]
        public void TestNumberPlayerAtStage()
        {
            string data = @"#Game No : 14296043526
            ***** Hand History for Game 14296043526 *****
            €4 EUR NL Texas Hold'em - Wednesday, December 24, 06:53:59 CEST 2014
            Table Livry-Gargan (Real Money)
            Seat 3 is the button
            Total number of players : 6/6
            Seat 1: petocreil60 ( €1.39 EUR )
            Seat 2: beubions ( €3.10 EUR )
            Seat 3: ricard62540 ( €4.01 EUR )
            Seat 4: SEBI284 ( €8.13 EUR )
            Seat 5: manille1 ( €3.98 EUR )
            Seat 6: gosth51 ( €3.94 EUR )
            SEBI284 posts small blind [€0.02 EUR].
            manille1 posts big blind [€0.04 EUR].
            beubions posts big blind [€0.04 EUR].
            ** Dealing down cards **
            gosth51 calls [€0.04 EUR]
            petocreil60 raises [€0.32 EUR]
            beubions folds
            ricard62540 folds
            SEBI284 raises [€1.18 EUR]
            manille1 folds
            gosth51 folds
            petocreil60 calls [€0.88 EUR]
            ** Dealing Flop ** [ 4h, Jc, 4d ]
            SEBI284 bets [€0.20 EUR]
            petocreil60 is all-In [€0.19 EUR]
            ** Dealing Turn ** [ Qh ]
            ** Dealing River ** [ 3s ]
            petocreil60 doesn't show [ 6s, 9s ]
            SEBI284 shows [ As, Kc ]
            SEBI284 wins €0.01 EUR from the side pot 1.
            SEBI284 wins €2.71 EUR from the main pot.";

            IHandHistoryParserFactory handHistoryParserFactory = new HandHistoryParserFactoryImpl();
            IHandHistoryParser parser = handHistoryParserFactory.GetFullHandHistoryParser(HandHistories.Objects.GameDescription.SiteName.PartyPokerFr);

            HandHistory history = parser.ParseFullHandHistory(data, false);

            Assert.AreEqual(2, PositionUtility.getNumberPlayerAtStage(history, HandHistories.Objects.Cards.Street.Preflop));
            Assert.AreEqual(2, PositionUtility.getNumberPlayerAtStage(history, HandHistories.Objects.Cards.Street.Flop));
            Assert.AreEqual(0, PositionUtility.getPosition(history, HandHistories.Objects.Cards.Street.Flop, "SEBI284"));
            Assert.AreEqual(0, PositionUtility.getPosition(history, HandHistories.Objects.Cards.Street.Preflop, "SEBI284"));
        }
    }
}
