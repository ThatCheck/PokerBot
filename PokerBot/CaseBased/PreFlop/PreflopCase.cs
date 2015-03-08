using PokerBot.CaseBased.Base;
using PokerBot.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.CaseBased.PreFlop
{
    public class PreflopCase : CaseBase
    {
        public PreflopCase(ActionEnum action, Double quality)
            : base(action, quality)
        {

        }
    }
}
