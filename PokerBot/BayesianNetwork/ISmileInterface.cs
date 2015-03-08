using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork
{
    interface ISmileInterface
    {
        String getCaseName();
        String[] getValueName();
        String[] getArcForValue();
    }
}
