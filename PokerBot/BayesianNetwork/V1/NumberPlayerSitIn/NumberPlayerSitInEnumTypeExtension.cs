using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.NumberPlayerSitIn
{
    public static class NumberPlayerSitInEnumTypeExtension
    {
        public static String ToBayesianNetwork(this NumberPlayerSitInEnumType numberPlayer)
        {
            switch (numberPlayer)
            {
                case NumberPlayerSitInEnumType.Two:
                    return "Two";
                case NumberPlayerSitInEnumType.Three:
                    return "Three";
                case NumberPlayerSitInEnumType.Four:
                    return "Four";
                case NumberPlayerSitInEnumType.Five:
                    return "Five";
                case NumberPlayerSitInEnumType.Six:
                    return "Six";
                default:
                    throw new ArgumentException("Unable to recognize the input NumberPlayerSitInEnumType");
            }
        }
    }
}
