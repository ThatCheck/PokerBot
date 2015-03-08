using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.CustomForm.Event
{
    public class ProgressEventArgs : EventArgs
    {
        public int Progress { get; set; }
        public int Max { get; set; }
        public ProgressEventArgs(int progress, int max)
        {
            Progress = progress;
            Max = max;
        }
    }
}
