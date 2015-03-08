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
            decimal pot = 0;
            decimal playerStack = 0;
            HandHistories.Objects.Cards.Street currentStreet = HandHistories.Objects.Cards.Street.Null;
            IEnumerable<HandHistories.Objects.Actions.HandAction> listHand = null;
            if (street == HandHistories.Objects.Cards.Street.Preflop)
            {
                listHand = list.Where(p => p.Street == HandHistories.Objects.Cards.Street.Preflop).OrderByDescending(p => p.ActionNumber);
            }
            else if (street == HandHistories.Objects.Cards.Street.Flop)
            {
                listHand = list.Where(p => p.Street == HandHistories.Objects.Cards.Street.Preflop || p.Street == HandHistories.Objects.Cards.Street.Flop).OrderByDescending(p => p.ActionNumber);
            }
            else if (street == HandHistories.Objects.Cards.Street.Turn)
            {
                listHand = list.Where(p => p.Street == HandHistories.Objects.Cards.Street.Preflop
                    || p.Street == HandHistories.Objects.Cards.Street.Flop
                    || p.Street == HandHistories.Objects.Cards.Street.Turn
                    ).OrderByDescending(p => p.ActionNumber);
            }
            else if (street == HandHistories.Objects.Cards.Street.River)
            {
                listHand = list.Where(p => p.Street == HandHistories.Objects.Cards.Street.Preflop
                    || p.Street == HandHistories.Objects.Cards.Street.Flop
                    || p.Street == HandHistories.Objects.Cards.Street.Turn
                    || p.Street == HandHistories.Objects.Cards.Street.River
                    ).OrderByDescending(p => p.ActionNumber);
            }
            foreach (HandHistories.Objects.Actions.HandAction hand in listHand)
            {
                if (currentStreet != hand.Street)
                {
                    currentStreet = hand.Street;
                    playerStack = 0;
                }
                pot += Math.Abs(hand.Amount);

                if (hand.PlayerName == player)
                {
                    playerStack += Math.Abs(hand.Amount);
                }
            }

            this.PotOdds = playerStack / pot;
            this.Value = this.PotOdds * 100;
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
