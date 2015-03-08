using HandHistories.Objects.Actions;
using PokerBot.Entity.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.Action
{
    public class Action : BaseAction
    {
        public Action(List<HandHistories.Objects.Actions.HandAction> list, String player) : base(list,player)
        {

        }

        public override Dictionary<string, ActionEnumType> getActionEnumDictionnary()
        {
            var playerActionList = this.ListPlayer.Where(p => p.Street == HandHistories.Objects.Cards.Street.Preflop).OrderBy(p => p.ActionNumber);
            Dictionary<String, ActionEnumType> dictionnaryHand = new Dictionary<String, ActionEnumType>();
            foreach (HandAction hand in playerActionList)
            {
                ActionEnumType lastAction = ActionEnumType.Check;
                if (hand.HandActionType == HandActionType.FOLD
                    && hand.HandActionType == HandActionType.SITTING_OUT)
                {
                    dictionnaryHand.Remove(hand.PlayerName);
                    continue;
                }

                if (hand.HandActionType == HandActionType.RAISE || hand.HandActionType == HandActionType.BET)
                {
                    int numberRaise = 0;
                    foreach (var handOfPlayer in dictionnaryHand)
                    {
                        if (handOfPlayer.Value == ActionEnumType.ThreeBet ||
                            handOfPlayer.Value == ActionEnumType.FourBet ||
                            handOfPlayer.Value == ActionEnumType.FiveBet)
                        {
                            numberRaise++;
                        }
                    }
                    lastAction = ActionEnumType.Raise;
                    if (numberRaise == 1)
                    {
                        lastAction = ActionEnumType.ThreeBet;
                    }
                    else if (numberRaise == 2)
                    {
                        lastAction = ActionEnumType.FourBet;
                    }
                    else if (numberRaise == 3)
                    {
                        lastAction = ActionEnumType.FiveBet;
                    }
                }
                if (hand.HandActionType == HandActionType.CALL)
                {
                    int numberRaise = 0;
                    foreach (var handOfPlayer in dictionnaryHand)
                    {
                        if (handOfPlayer.Value == ActionEnumType.Raise ||
                            handOfPlayer.Value == ActionEnumType.ThreeBet ||
                            handOfPlayer.Value == ActionEnumType.FourBet ||
                            handOfPlayer.Value == ActionEnumType.FiveBet)
                        {
                            numberRaise++;
                        }
                    }
                    lastAction = ActionEnumType.Call;
                    if (numberRaise == 1)
                    {
                        lastAction = ActionEnumType.CallRaise;
                    }
                    else if (numberRaise == 2)
                    {
                        lastAction = ActionEnumType.CallThreeBet;
                    }
                    else if (numberRaise == 3)
                    {
                        lastAction = ActionEnumType.CallFourBet;
                    }
                    else if (numberRaise == 4)
                    {
                        lastAction = ActionEnumType.CallFiveBet;
                    }
                }
                if (hand.HandActionType == HandActionType.CHECK)
                {
                    lastAction = ActionEnumType.Check;
                }
                if (!dictionnaryHand.ContainsKey(hand.PlayerName))
                {
                    dictionnaryHand.Add(hand.PlayerName, lastAction);
                }
                dictionnaryHand[hand.PlayerName] = lastAction;
            }
            return dictionnaryHand;
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
                ActionEnumType.Bet,
                ActionEnumType.Call,
                ActionEnumType.CallFiveBet,
                ActionEnumType.CallFourBet,
                ActionEnumType.CallRaise,
                ActionEnumType.CallThreeBet,
                ActionEnumType.Check,
                ActionEnumType.FiveBet,
                ActionEnumType.FourBet,
                ActionEnumType.Raise,
                ActionEnumType.ThreeBet,
            };
            foreach(ActionEnumType action in actionEnumArray)
            {
                data.Add(action.ToBayesianNetwork());
            }
            return data.ToArray();
        }

        public new static string[] getArcForValue()
        {
            return new String[] { typeof(ActionFlop).Name, typeof(HandGroup.HandGroup).Name};
        }
    }
}
