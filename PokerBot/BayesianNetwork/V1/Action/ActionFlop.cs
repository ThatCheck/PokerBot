using HandHistories.Objects.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.Action
{
    public class ActionFlop : BaseAction
    {
        protected Dictionary<String, ActionEnumType> _precedent;

        public ActionFlop(List<HandHistories.Objects.Actions.HandAction> list, String player) : base(list,player)
        {
            this._currentStreet = HandHistories.Objects.Cards.Street.Flop;
            this._precedent = new Action(list,player).getActionEnumDictionnary();
        }

        public override Dictionary<string, ActionEnumType> getActionEnumDictionnary()
        {
            var playerActionList = this.ListPlayer.Where(p => p.Street == this._currentStreet).OrderBy(p => p.ActionNumber);
            String playerNameForRaise = null;
            Dictionary<String, ActionEnumType> dictionnaryHand = new Dictionary<String, ActionEnumType>();

            foreach (var precedentHand in this._precedent)
            {
                if(precedentHand.Value.isAggessiveAction())
                {
                    playerNameForRaise = precedentHand.Key;
                }
            }

            foreach (HandAction hand in playerActionList)
            {
                ActionEnumType lastAction = ActionEnumType.Call;
                if (hand.HandActionType == HandActionType.FOLD
                   || hand.HandActionType == HandActionType.SITTING_OUT)
                {
                    dictionnaryHand.Remove(hand.PlayerName);
                    continue;
                }
                //So now, we need to define if it's a cbet or a donkbet or nothing
                if (playerNameForRaise != null)
                {
                    if (hand.HandActionType == HandActionType.BET)
                    {
                        if (playerNameForRaise == hand.PlayerName)
                        {
                            lastAction = ActionEnumType.ContinuationBet;
                        }
                        else
                        {
                            lastAction = ActionEnumType.DonkBet;
                        }
                        playerNameForRaise = null;
                    }
                    if (playerNameForRaise == hand.PlayerName)
                    {
                        playerNameForRaise = null;
                    }

                }
                if (hand.HandActionType == HandActionType.RAISE)
                {
                    lastAction = ActionEnumType.Bet;
                    if (dictionnaryHand.Any(p => p.Value == ActionEnumType.FourBet))
                    {
                        lastAction = ActionEnumType.FiveBet;
                    }
                    else if (dictionnaryHand.Any(p => p.Value == ActionEnumType.ThreeBet))
                    {
                        lastAction = ActionEnumType.FourBet;
                    }
                    else if (dictionnaryHand.Any(p => p.Value == ActionEnumType.Raise))
                    {
                        lastAction = ActionEnumType.ThreeBet;
                    }
                    else if (dictionnaryHand.Any( p => p.Value == ActionEnumType.Bet || p.Value == ActionEnumType.ContinuationBet || p.Value == ActionEnumType.DonkBet))
                    {
                        lastAction = ActionEnumType.Raise;
                    }
                }
                else if (hand.HandActionType == HandActionType.CALL)
                {
                    lastAction = ActionEnumType.Call;
                    if (dictionnaryHand.Any(p => p.Value == ActionEnumType.FiveBet))
                    {
                        lastAction = ActionEnumType.CallFiveBet;
                    }
                    else if (dictionnaryHand.Any(p => p.Value == ActionEnumType.FourBet))
                    {
                        lastAction = ActionEnumType.CallFourBet;
                    }
                    else if (dictionnaryHand.Any(p => p.Value == ActionEnumType.ThreeBet))
                    {
                        lastAction = ActionEnumType.CallThreeBet;
                    }
                    else if (dictionnaryHand.Any(p => p.Value == ActionEnumType.Raise))
                    {
                        lastAction = ActionEnumType.CallRaise;
                    }
                }
                else if (hand.HandActionType == HandActionType.CHECK)
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
                ActionEnumType.Call,
                ActionEnumType.CallFiveBet,
                ActionEnumType.CallFourBet,
                ActionEnumType.CallRaise,
                ActionEnumType.CallThreeBet,
                ActionEnumType.Check,
                ActionEnumType.Bet,
                ActionEnumType.FiveBet,
                ActionEnumType.FourBet,
                ActionEnumType.Raise,
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
            return new String[] { typeof(ActionTurn).Name, typeof(HandType.HandType).Name, typeof(DrawingHand.DrawingHand).Name };
        }
    }
}
