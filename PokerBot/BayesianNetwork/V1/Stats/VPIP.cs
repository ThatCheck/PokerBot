using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.Stats
{
    public class VPIP : BornedValue<VPIP>
    {
        private static int[] bornVPIP = new int[] { 0, 10, 20, 30, 40, 50, 60, 100 };
       
        public new static int[] getBoundary()
        {
            return bornVPIP;
        }

        public VPIP(decimal vpip)
            : base(vpip, false)
        {

        }

        public new static string[] getArcForValue()
        {
            return new String[] { typeof(HandGroup.HandGroup).Name };
        }
    }
}
