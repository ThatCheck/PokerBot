using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.Action
{
    public enum ActionEnumType
    {
        Check,
        Call,
        Bet,
        ContinuationBet,
        DonkBet,
        Raise,
        CallRaise,
        ThreeBet,
        CallThreeBet,
        FourBet,
        CallFourBet,
        FiveBet,
        CallFiveBet
    }
}
