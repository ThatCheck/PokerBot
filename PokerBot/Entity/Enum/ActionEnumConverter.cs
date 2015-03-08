using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Entity.Enum
{
    public static class ActionEnumConverter
    {
        public static String FromActionEnum(ActionEnum action)
        {
            switch (action)
            {
                case ActionEnum.AllIn:
                    return "ai";
                case ActionEnum.BetHalf:
                    return "1/2b";
                case ActionEnum.BetPot:
                    return "1b";
                case ActionEnum.BetQuarter:
                    return "1/4b";
                case ActionEnum.BetThreeQuarter:
                    return "3/4b";
                case ActionEnum.Call:
                    return "ca";
                case ActionEnum.Check:
                    return "ch";
                case ActionEnum.Fold:
                    return "f";
                case ActionEnum.Overbet:
                    return "ob";
                default:
                    return null;
            }
        }
    }
}
