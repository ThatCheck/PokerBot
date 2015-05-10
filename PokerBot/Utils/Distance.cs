using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Utils
{
    public static class Distance
    {
        public static double EuclidianDistance(double one, double two, double max)
        {
            return 1 - (Math.Abs(one - two)) / max;
        }

        public static double ExponentialDecay(double one, double two, double max, double k)
        {
            return Math.Exp( -k * (Math.Abs(one - two) / max));
        }
    }
}
