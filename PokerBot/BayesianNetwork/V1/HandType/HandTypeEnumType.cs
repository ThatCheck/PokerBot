using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.HandType
{
    public enum HandTypeEnumType
    {
        AcePairStrong,
        AcePairWeak,
        Busted,
        Flush,
        FourOfAKind,
        FullHouse,
        JackPair,
        KingPairStrong,
        KingPairWeak,
        LowPair,
        MidPair,
        QueenPair,
        Straight,
        StraightFlush,
        TenPair,
        ThreeOfAKind,
        TwoPair
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
