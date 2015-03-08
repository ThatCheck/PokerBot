using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.DrawingHand
{
    public static class DrawingHandEnumTypeExtensions
    {
        public static String ToBayesianNetwork(this DrawingHandEnumType drawhingHandEnum)
        {
            switch (drawhingHandEnum)
            {
                case DrawingHandEnumType.DrawHit:
                    return "DrawHit";
                case DrawingHandEnumType.FlushDraw:
                    return "FlushDraw";
                case DrawingHandEnumType.NoDraw:
                    return "NoDraw";
                case DrawingHandEnumType.StraightDraw:
                    return "StraightDraw";
                case DrawingHandEnumType.StraightD_FlushD:
                    return "StraightD_FlushD";
                default:
                    throw new ArgumentException("Unable to recognize the input DrawingHandEnumType");
            }
        }
    }
}
