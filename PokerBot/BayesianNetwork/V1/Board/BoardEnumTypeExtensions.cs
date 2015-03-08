using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.Board
{
    public static class BoardEnumTypeExtensions
    {
        public static String ToBayesianNetwork(this BoardEnumType boardEnum)
        {
            switch (boardEnum)
            {
                case BoardEnumType.AHigh:
                    return "AHigh";
                case BoardEnumType.ALow:
                    return "ALow";
                case BoardEnumType.FirstA:
                    return "FirstA";
                case BoardEnumType.FirstJ:
                    return "FirstJ";
                case BoardEnumType.FirstK:
                    return "FirstK";
                case BoardEnumType.FirstQ:
                    return "FirstQ";
                case BoardEnumType.FourOfAKind:
                    return "FourOfAKind";
                case BoardEnumType.FullHouse:
                    return "FullHouse";
                case BoardEnumType.HighPair:
                    return "HighPair";
                case BoardEnumType.JLow:
                    return "JLow";
                case BoardEnumType.KHigh:
                    return "KHigh";
                case BoardEnumType.KLow:
                    return "KLow";
                case BoardEnumType.Low:
                    return "Low";
                case BoardEnumType.LowPair:
                    return "LowPair";
                case BoardEnumType.MidPair:
                    return "MidPair";
                case BoardEnumType.QHigh:
                    return "QHigh";
                case BoardEnumType.QLow:
                    return "QLow";
                case BoardEnumType.ThreeOfAKind:
                    return "ThreeOfAKind";
                case BoardEnumType.TwoPair:
                    return "TwoPair";
                default:
                    throw new ArgumentException("Unable to recognize the input BoardEnumType");
            }
        }
    }
}
