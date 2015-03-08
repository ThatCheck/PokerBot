using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerBot.Hand;
using System.Diagnostics;

namespace PokerBotTest.Benchmark
{
    [TestClass]
    public class TwoPlusTwoHandEvaluatorTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            int[] str = new int[] { 52, 34, 25, 18, 1, 37, 22 };

            TwoPlusTwoHandEvaluatorResult r1;

            DateTime now = DateTime.Now;
            for (int i = 0; i < 10000000; i++) // 10 mil iterations 1.5 - 2 sec
            { 
                r1 = TwoPlusTwoHandEvaluator.Instance.LookupHand7(str); 
            } 
            TimeSpan s1 = DateTime.Now - now;
            Debug.WriteLine("TOTAL TIME ONE 10 000 000 ite : " + s1.TotalMilliseconds);
        }
    }
}
