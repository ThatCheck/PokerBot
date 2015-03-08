using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.Stats
{
    public class ThreeBet : BornedValue<ThreeBet>
    {
        private static int[] bornThreeBet = new int[] { 0, 3, 5, 7, 10, 15 };

        public new static int[] getBoundary()
        {
            return bornThreeBet;
        }

        public ThreeBet(decimal threebet)
            : base(threebet, true)
        {

        }
        public new static string[] getArcForValue()
        {
            return new String[] { typeof(HandGroup.HandGroup).Name };
        }
    }
}
