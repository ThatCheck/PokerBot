using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.Stats
{
    public class PFR : BornedValue<PFR>
    {
        private static int[] bornPFR = new int[] { 0, 5, 10, 15, 20, 25 };

        public new static int[] getBoundary()
        {
            return bornPFR;
        }

        public PFR(decimal pfr)
            : base(pfr, true)
        {

        }
        public new static string[] getArcForValue()
        {
            return new String[] { typeof(HandGroup.HandGroup).Name };
        }
    }
}
