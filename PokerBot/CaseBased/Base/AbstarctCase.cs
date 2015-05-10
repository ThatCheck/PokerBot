using PokerBot.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.CaseBased.Base
{
    public class DecisionCase : CaseBase
    {
        private ActionEnum _action;
        private Double _quality;

        public Double Quality
        {
            set { this._quality = value; }
            get { return _quality; }
        }

        public ActionEnum Action
        {
            set { this._action = value; }
            get { return _action; }
        }

        public DecisionCase()
        {

        }

        public override double similarityTo(CaseBase pfCaseTemp)
        {
            throw new NotImplementedException();
        }
    }
}
