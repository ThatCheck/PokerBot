using HandHistories.Objects.Hand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Entity.Event
{
    public class DecodedHandEventArgs : EventArgs
    {
        private HandHistory _handHistory;

        public HandHistory HandHistory
        {
            get { return _handHistory; }
            set { _handHistory = value; }
        }

        public DecodedHandEventArgs(HandHistory hand)
        {
            this._handHistory = hand;
        }
    }
}
