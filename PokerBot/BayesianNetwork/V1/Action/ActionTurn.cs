using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.Action
{
    public class ActionTurn : ActionFlop
    {
        public ActionTurn(List<HandHistories.Objects.Actions.HandAction> list, String player) : base(list,player)
        {
            this._currentStreet = HandHistories.Objects.Cards.Street.Turn;
            this._precedent = new ActionFlop(list, player).getActionEnumDictionnary();
        }


        public new static string getCaseName()
        {
            return System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        }

        public new static string[] getValueName()
        {
            List<String> data = new List<string>();
            ActionEnumType[] actionEnumArray = new ActionEnumType[]
            {
                ActionEnumType.Call,
                ActionEnumType.CallFiveBet,
                ActionEnumType.CallFourBet,
                ActionEnumType.CallRaise,
                ActionEnumType.CallThreeBet,
                ActionEnumType.Check,
                ActionEnumType.FiveBet,
                ActionEnumType.FourBet,
                ActionEnumType.Raise,
                ActionEnumType.Bet,
                ActionEnumType.ThreeBet,
                ActionEnumType.ContinuationBet,
                ActionEnumType.DonkBet
            };
            foreach (ActionEnumType action in actionEnumArray)
            {
                data.Add(action.ToBayesianNetwork());
            }
            return data.ToArray();
        }

        public new static string[] getArcForValue()
        {
            return new String[] { typeof(ActionRiver).Name, typeof(HandType.HandTypeTurn).Name };
        }
    }
}
