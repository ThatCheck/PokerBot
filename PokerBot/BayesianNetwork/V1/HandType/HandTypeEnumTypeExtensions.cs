using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.HandType
{
    public static class HandTypeEnumTypeExtensions
    {
        public static String ToBayesianNetwork(this HandTypeEnumType handTypeEnum)
        {
            switch (handTypeEnum)
            {
                case HandTypeEnumType.AcePairStrong:
                    return "AcePairStrong";
                case HandTypeEnumType.AcePairWeak:
                    return "AcePairWeak";
                case HandTypeEnumType.Busted:
                    return "Busted";
                case HandTypeEnumType.Flush:
                    return "Flush";
                case HandTypeEnumType.FourOfAKind:
                    return "FourOfAKind";
                case HandTypeEnumType.FullHouse:
                    return "FullHouse";
                case HandTypeEnumType.JackPair:
                    return "JackPair";
                case HandTypeEnumType.KingPairStrong:
                    return "KingPairStrong";
                case HandTypeEnumType.KingPairWeak:
                    return "KingPairWeak";
                case HandTypeEnumType.LowPair:
                    return "LowPair";
                case HandTypeEnumType.MidPair:
                    return "MidPair";
                case HandTypeEnumType.QueenPair:
                    return "QueenPair";
                case HandTypeEnumType.Straight:
                    return "Straight";
                case HandTypeEnumType.StraightFlush:
                    return "StraightFlush";
                case HandTypeEnumType.TenPair:
                    return "TenPair";
                case HandTypeEnumType.ThreeOfAKind:
                    return "ThreeOfAKind";
                case HandTypeEnumType.TwoPair:
                    return "TwoPair";
                default:
                    throw new ArgumentException("Unable to recognize value for HandTypeEnumTypeExtensions");
            }
        }
    }
}
