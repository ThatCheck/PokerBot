using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.PotOdds
{
    public abstract class BasePotOdds : BornedValue<BasePotOdds>
    {
        private decimal _potOdds;

        public decimal PotOdds
        {
            get { return _potOdds; }
            set { _potOdds = value; }
        }

        public new static int[] getBoundary()
        {
            return new int[] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
        }

        public BasePotOdds(List<HandHistories.Objects.Actions.HandAction> list, String player, HandHistories.Objects.Cards.Street street) : base(0,false)
        {
            try
            {
                var lastActionOfPlayerInStreet = list.Where(p => p.Street == street && p.PlayerName == player).OrderBy(p => p.ActionNumber).Last();
                decimal potSize = list.Where(p => p.Street <= street && p.ActionNumber < lastActionOfPlayerInStreet.ActionNumber).Sum(p => Math.Abs(p.Amount));

                decimal playerStack = Math.Abs(lastActionOfPlayerInStreet.Amount);
                this.PotOdds = playerStack / potSize;
                this.Value = this.PotOdds * 100;
            }
            catch (Exception ex)
            {

            }
        }

        public new static string getCaseName()
        {
            return System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        }

        public new static string[] getValueName()
        {
            List<String> data = new List<string>();
            foreach (int value in new int[] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 })
            {
                data.Add(BornedValue<BasePotOdds>.getValueFromInt(value));
            }
            return data.ToArray();
        }

        public new static string[] getArcForValue()
        {
            throw new NotImplementedException();
        }
    }
}
