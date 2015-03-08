using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.DrawingHand
{
    public enum StateFlushStraightEnumType
    {
        Draw,
        None,
        Possible,
        Draw_Draw,
        Draw_Possible,
        Draw_Likely,
        Draw_Straight,
        None_Possible,
        None_Draw,
        None_None,
        Possible_Likely,
        Possible_Possible,
        Possible_Straight,
        Likely_Likely,
        Likely_Flush,
        Likely_Straight
    }
}
