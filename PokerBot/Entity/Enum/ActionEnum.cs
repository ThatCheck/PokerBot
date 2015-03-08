using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Entity.Enum
{
    public enum ActionEnum
    {
        Fold,
        Check,
        Call,
        BetQuarter,
        BetHalf,
        BetThreeQuarter,
        BetPot,
        Overbet,
        AllIn,
        Other
    }
}
