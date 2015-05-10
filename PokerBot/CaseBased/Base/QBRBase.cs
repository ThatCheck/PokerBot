using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.CaseBased.Base
{
    public sealed class QBRBase
    {
        private Dictionary<String, List<CaseBase>> _listCaseBase;

        public Dictionary<String, List<CaseBase>> ListCaseBase
        {
            get { return _listCaseBase; }
            set { _listCaseBase = value; }
        }

        public QBRBase()
        {
            this._listCaseBase = new Dictionary<String, List<CaseBase>>();
        }

        public List<CaseBase> find(String hashsetID, double threshold, CaseBase refCase)
        {
            List<CaseBase> list = new List<CaseBase>();
            if (this._listCaseBase.ContainsKey(hashsetID))
            {
                foreach (var caseValue in this._listCaseBase[hashsetID])
                {
                    if (refCase.similarityTo(caseValue) * 100 > threshold)
                    {
                        list.Add(caseValue);
                    }
                }
            }
            return list;
        }

        public void AddCase(String hashsetID, CaseBase caseDetails)
        {
            if(!this._listCaseBase.ContainsKey(hashsetID))
            {
                this._listCaseBase.Add(hashsetID, new List<CaseBase>());
            }

            this._listCaseBase[hashsetID].Add(caseDetails);
        }

        public void fromIEnumerable(IEnumerable<CaseBase> enumerable)
        {
            foreach (CaseBase instance in enumerable)
            {
                this.AddCase(instance.BetPattern, instance);
            }
        }

        public void serialize(string to)
        {
             BinaryFormatter bin = new BinaryFormatter();
             using (Stream stream = File.Open(to, FileMode.Create))
             {
                 foreach (var listCase in this.ListCaseBase)
                 {
                     bin.Serialize(stream, listCase.Value);
                 }
             }
        }

        public void unserialize(string from)
        {
            long position = 0;
            BinaryFormatter bin = new BinaryFormatter();
            using (Stream stream = File.Open(from, FileMode.Open))
            {
                while(position < stream.Length){
                    stream.Seek(position, SeekOrigin.Begin);
                    List<CaseBase> caseList = (List<CaseBase>)bin.Deserialize(stream);
                    this.ListCaseBase.Add(caseList.First().BetPattern, caseList);
                    position = stream.Position;
                }
            }
        }
    }
}
