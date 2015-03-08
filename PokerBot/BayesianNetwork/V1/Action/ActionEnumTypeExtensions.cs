using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.Action
{
    public static class ActionEnumTypeExtensions
    {
        public static String ToBayesianNetwork(this ActionEnumType actionType)
        {
            switch (actionType)
            {
                case ActionEnumType.Call :
                    return "Call";
                case ActionEnumType.CallFiveBet:
                    return "CallFiveBet";
                case ActionEnumType.CallFourBet:
                    return "CallFourBet";
                case ActionEnumType.CallRaise:
                    return "CallRaise";
                case ActionEnumType.CallThreeBet:
                    return "CallThreeBet";
                case ActionEnumType.Check:
                    return "Check";
                case ActionEnumType.FiveBet:
                    return "FiveBet";
                case ActionEnumType.FourBet:
                    return "FourBet";
                case ActionEnumType.Raise:
                    return "Raise";
                case ActionEnumType.ThreeBet:
                    return "ThreeBet";
                case ActionEnumType.ContinuationBet:
                    return "ContinuationBet";
                case ActionEnumType.DonkBet:
                    return "DonkBet";
                case ActionEnumType.Bet:
                    return "Bet";
                default:
                    throw new ArgumentException("Unable to recognize the input ActionEnumType");
            }
        }

        public static Boolean isAggessiveAction(this ActionEnumType actionType)
        {
            return actionType == ActionEnumType.Bet
                || actionType == ActionEnumType.ContinuationBet
                || actionType == ActionEnumType.DonkBet
                || actionType == ActionEnumType.FiveBet
                || actionType == ActionEnumType.FourBet
                || actionType == ActionEnumType.Raise
                || actionType == ActionEnumType.ThreeBet;
        }
    }
}
