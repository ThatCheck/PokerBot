using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.HandGroup
{
    public static class HandGroupEnumTypeExtensions
    {
        public static String ToBayesianNetwork(this HandGroupEnumType handGroupEnum)
        {
            switch (handGroupEnum)
            {
                case HandGroupEnumType.HandGroup1:
                    return "HandGroup1";
                case HandGroupEnumType.HandGroup2:
                    return "HandGroup2";
                case HandGroupEnumType.HandGroup3:
                    return "HandGroup3";
                case HandGroupEnumType.HandGroup4:
                    return "HandGroup4";
                case HandGroupEnumType.HandGroup5:
                    return "HandGroup5";
                case HandGroupEnumType.HandGroup6:
                    return "HandGroup6";
                case HandGroupEnumType.HandGroup7:
                    return "HandGroup7";
                case HandGroupEnumType.HandGroup8:
                    return "HandGroup8";
                case HandGroupEnumType.HandGroup9:
                    return "HandGroup9";
                case HandGroupEnumType.HandGroup10:
                    return "HandGroup10";
                default:
                    throw new ArgumentException("Unable to recognize value for HandGroupEnumType");
            }
        }
    }
}
