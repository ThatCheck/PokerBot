using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.Position
{
    public static class PositionEnumTypeExtensions
    {
        public static String ToBayesianNetwork(this PositionEnumType position)
        {
            switch (position)
            {
                case PositionEnumType.First:
                    return "OnePlayer";
                case PositionEnumType.Second:
                    return "TwoPlayer";
                case PositionEnumType.Third:
                    return "ThreePlayer";
                case PositionEnumType.Fourth:
                    return "FourPlayer";
                case PositionEnumType.Fifth:
                    return "FivePlayer";
                case PositionEnumType.Sixth:
                    return "SixPlayer";
                default:
                    throw new ArgumentException("Unable to recognize the input PositionEnumType");
            }
        }
    }
}
