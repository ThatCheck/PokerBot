using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.HandType
{
    public enum HandTypeEnumType
    {
        AcePairStrong = 9,
        AcePairWeak = 8,
        Busted = 0,
        Flush = 13,
        FourOfAKind = 15,
        FullHouse = 14,
        JackPair = 4,
        KingPairStrong = 7,
        KingPairWeak = 6,
        LowPair = 1,
        MidPair = 2,
        QueenPair = 5,
        Straight = 12,
        StraightFlush = 16,
        TenPair = 3,
        ThreeOfAKind = 11,
        TwoPair = 10
    }

    public static class ToHandType
    {
        public static HandTypeEnumType toHandType(string from)
        {
            switch (from)
            {
                case "AcePairStrong":
                    return HandTypeEnumType.AcePairStrong;
                case "AcePairWeak":
                    return HandTypeEnumType.AcePairWeak;
                case "Busted":
                    return HandTypeEnumType.Busted;
                case "Flush":
                    return HandTypeEnumType.Flush;
                case "FourOfAKind":
                    return HandTypeEnumType.FourOfAKind;
                case "FullHouse":
                    return HandTypeEnumType.FullHouse;
                case "JackPair":
                    return HandTypeEnumType.JackPair;
                case "KingPairStrong":
                    return HandTypeEnumType.KingPairStrong;
                case "KingPairWeak":
                    return HandTypeEnumType.KingPairWeak;
                case "LowPair":
                    return HandTypeEnumType.LowPair;
                case "MidPair":
                    return HandTypeEnumType.MidPair;
                case "QueenPair":
                    return HandTypeEnumType.QueenPair;
                case "Straight":
                    return HandTypeEnumType.Straight;
                case "StraightFlush":
                    return HandTypeEnumType.StraightFlush;
                case "TenPair":
                    return HandTypeEnumType.TenPair;
                case "ThreeOfAKind":
                    return HandTypeEnumType.ThreeOfAKind;
                case "TwoPair":
                    return HandTypeEnumType.TwoPair;
                default:
                    throw new ArgumentException("Unable to recognize value for HandTypeEnumTypeExtensions : " + from);
            }
        }
    }
}
