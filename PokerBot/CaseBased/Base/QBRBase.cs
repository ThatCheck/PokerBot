using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.CaseBased.Base
{
    public class QBRBase
    {
        /**
         * String represent the index key for research
         * List<caseBase> represent a list of case based to iterate throught
         */

        private Dictionary<String,List<CaseBase>> _listCaseBase;

        public Dictionary<String, List<CaseBase>> ListCaseBase
        {
            get { return _listCaseBase; }
        }

        public QBRBase()
        {
            this._listCaseBase = new Dictionary<String, List<CaseBase>>();
        }

        public void AddCase(String hashsetID, CaseBase caseDetails)
        {
            if(!this._listCaseBase.ContainsKey(hashsetID))
            {
                this._listCaseBase.Add(hashsetID, new List<CaseBase>());
            }

            this._listCaseBase[hashsetID].Add(caseDetails);
        }
    }
}
