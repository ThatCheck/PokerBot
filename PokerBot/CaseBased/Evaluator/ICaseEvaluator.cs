using HandHistories.Objects.Hand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.CaseBased.Evaluator
{
    public interface ICaseEvaluator
    {
        Double getQuality(HandHistory handHistory,String playerName);
    }
}
