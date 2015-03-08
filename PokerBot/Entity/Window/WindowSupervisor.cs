using PokerBot.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Entity.Window
{
    public class WindowSupervisor
    {
        private ObservableDictionary<IntPtr, Table.Table> _listActiveTable;

        public ObservableDictionary<IntPtr, Table.Table> TableList
        {
            get
            {
                return this._listActiveTable;
            }
        }

        public WindowSupervisor()
        {
            this._listActiveTable = new ObservableDictionary<IntPtr, Table.Table>();
        }

        public void discoverTable()
        {
            IEnumerable<IntPtr> listTable = WindowHandle.FindAllPokerTableWindow();
            List<IntPtr> foundOrCreate = new List<IntPtr>();
            foreach (IntPtr ptrAddr in listTable)
            {
                if (!this._listActiveTable.ContainsKey(ptrAddr))
                {
                    this._listActiveTable.Add(ptrAddr, new Table.Table(ptrAddr));
                }

                foundOrCreate.Add(ptrAddr);
            }

            foreach (KeyValuePair<IntPtr, Table.Table> kvp in this._listActiveTable)
            {
                if (!foundOrCreate.Contains(kvp.Key))
                {
                    this._listActiveTable.Remove(kvp.Key);
                }
            }
        }

    }
}
