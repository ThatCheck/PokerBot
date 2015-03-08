using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.Stats
{
    public class FoldToThreeBet : BornedValue<FoldToThreeBet>
    {
        private static int[] bornFoldToThreeBet = new int[] { 0, 20, 40, 60, 80, 100 };

        public new static int[] getBoundary()
        {
            return bornFoldToThreeBet;
        }

        public FoldToThreeBet(decimal FoldToThreeBet)
            : base(FoldToThreeBet, false)
        {

        }

        public new static string[] getArcForValue()
        {
            return new String[] {  typeof(HandGroup.HandGroup).Name };
        }
    }
}
