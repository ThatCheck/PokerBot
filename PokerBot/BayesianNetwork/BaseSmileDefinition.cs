using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork
{
    public abstract class BaseSmileDefinition
    {
        public static string getCaseName()
        {
            throw new NotImplementedException();
        }

        public static string[] getValueName()
        {
            throw new NotImplementedException();
        }

        public static string[] getArcForValue()
        {
            throw new NotImplementedException();
        }
    }
}
