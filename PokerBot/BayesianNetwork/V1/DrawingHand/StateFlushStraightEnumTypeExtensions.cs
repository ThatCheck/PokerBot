using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.DrawingHand
{
    public static class StateFlushStraightEnumTypeExtensions
    {
        public static String ToBayesianNetwork(this StateFlushStraightEnumType stateFlushStraightEnum)
        {
            switch (stateFlushStraightEnum)
            {
                case StateFlushStraightEnumType.Draw:
                    return "Draw";
                case StateFlushStraightEnumType.None:
                    return "None";
                case StateFlushStraightEnumType.Possible:
                    return "Possible";
                case StateFlushStraightEnumType.Draw_Straight:
                    return "Draw_Straight";
                case StateFlushStraightEnumType.Draw_Draw:
                    return "Draw_Draw";
                case StateFlushStraightEnumType.Draw_Possible:
                    return "Draw_Possible";
                case StateFlushStraightEnumType.Draw_Likely:
                    return "Draw_Likely";
                case StateFlushStraightEnumType.None_Possible:
                    return "None_Possible";
                case StateFlushStraightEnumType.None_Draw:
                    return "None_Draw";
                case StateFlushStraightEnumType.None_None:
                    return "None_None";
                case StateFlushStraightEnumType.Possible_Likely:
                    return "Possible_Likely";
                case StateFlushStraightEnumType.Possible_Possible:
                    return "Possible_Possible";
                case StateFlushStraightEnumType.Possible_Straight:
                    return "Possible_Straight";
                case StateFlushStraightEnumType.Likely_Likely:
                    return "Likely_Likely";
                case StateFlushStraightEnumType.Likely_Flush:
                    return "Likely_Flush";
                case StateFlushStraightEnumType.Likely_Straight:
                    return "Likely_Straight";
                default:
                    throw new ArgumentException("Unable to recognize the input StateFlushStraightEnumType");
            }
        }
    }
}
