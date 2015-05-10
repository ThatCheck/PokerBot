using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HandHistories.Parser.Parsers.Factory;
using HandHistories.Parser.Parsers.Base;
using HandHistories.Objects.Hand;
using PokerBot.CaseBased.Trainer;
using PokerBot.CaseBased.PreFlop;
using System.Collections.Generic;

namespace PokerBotTest.CBR
{
    [TestClass]
    public class CBRTest
    {
        [TestMethod]
        public void CBRCaseRangeGenerator()
        {
            string data = @"#Game No : 14296344076
                ***** Hand History for Game 14296344076 *****
                €4 EUR NL Texas Hold'em - Wednesday, December 24, 13:39:06 CEST 2014
                Table Bourges (Real Money)
                Seat 3 is the button
                Total number of players : 6/6
                Seat 1: tata-suzanne ( €4.19 EUR )
                Seat 2: chris13888 ( €4.03 EUR )
                Seat 3: catalane 31 ( €1.85 EUR )
                Seat 4: FRANCK720757 ( €1.92 EUR )
                Seat 5: prigo666 ( €3.70 EUR )
                Seat 6: lucadulao ( €1.90 EUR )
                FRANCK720757 posts small blind [€0.02 EUR].
                prigo666 posts big blind [€0.04 EUR].
                ** Dealing down cards **
                lucadulao folds
                tata-suzanne calls [€0.04 EUR]
                chris13888 folds
                catalane 31 folds
                FRANCK720757 folds
                prigo666 checks
                ** Dealing Flop ** [ As, 7s, Kc ]
                prigo666 checks
                tata-suzanne bets [€0.07 EUR]
                prigo666 calls [€0.07 EUR]
                ** Dealing Turn ** [ 6d ]
                prigo666 checks
                tata-suzanne checks
                ** Dealing River ** [ 4h ]
                prigo666 checks
                tata-suzanne checks
                tata-suzanne doesn't show [ 3s, 2s ]
                prigo666 shows [ Ah, 2d ]
                prigo666 wins €0.23 EUR from the main pot.";

            IHandHistoryParserFactory handHistoryParserFactory = new HandHistoryParserFactoryImpl();
            IHandHistoryParser parser = handHistoryParserFactory.GetFullHandHistoryParser(HandHistories.Objects.GameDescription.SiteName.PartyPokerFr);

            HandHistory history = parser.ParseFullHandHistory(data, false);

            //List<PreflopCase> pfCases = TrainerCase.generatePreFlopCaseForHand(history, new System.Collections.Generic.List<string>() { "prigo666" });
            //Assert.AreEqual(5, pfCases.Count);
        }
    }
}
