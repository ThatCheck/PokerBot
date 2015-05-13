using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaysianNetworkRepartitor.SmileNetwork
{
    public class SmileNetwork
    {
        #region Network
        Smile.Network _network;

        public Smile.Network Network
        {
            get { return _network; }
        }
        #endregion

        public SmileNetwork(String networkFile) : base()
        {
            try
            {
                this._network = new Smile.Network();
                this._network.ReadFile(networkFile);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<String> request(List<Tuple<String, String>> data)
        {
            this._network.ClearAllEvidence();
            string toAnalyze = "";
            foreach(var evidence in data)
            {
                if (evidence.Item1.Equals("Analyze"))
                {
                    toAnalyze = evidence.Item2;
                }
                else
                {
                    this._network.SetEvidence(evidence.Item1, evidence.Item2);
                }
            }

            this._network.UpdateBeliefs();
            var result = this._network.GetNodeValue(toAnalyze);
            String[] aSuccessOutcomeIds = this._network.GetOutcomeIds(toAnalyze);
            Dictionary<String, double> returnValue = new Dictionary<string, double>();
            for (int outcomeIndex = 0; outcomeIndex < aSuccessOutcomeIds.Length; outcomeIndex++)
            {
                returnValue.Add(aSuccessOutcomeIds[outcomeIndex], result[outcomeIndex]);
            }

            return returnValue.OrderByDescending(p => p.Value).Select( p => p.Key);
        }
    }
}
