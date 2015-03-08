using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Entity.Event
{
    public class DecodedErrorHandEventArgs
    {
        private String _tableData;
        private Exception _ex;

        public Exception Ex
        {
            get { return _ex; }
            set { _ex = value; }
        }

        public String TableData
        {
            get { return _tableData; }
            set { _tableData = value; }
        }

        public DecodedErrorHandEventArgs(String tableData, Exception ex)
        {
            this._ex = ex;
            this._tableData = tableData;
        }
    }
}
