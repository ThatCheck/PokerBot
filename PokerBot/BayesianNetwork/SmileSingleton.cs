using PokerBot.BayesianNetwork.V1;
using PokerBot.Entity.Table;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork
{
    public class SmileSingleton
    {
        private static SmileSingleton instance;
        private static object syncRoot = new Object(); 
        private static readonly object syncLock = new object();
        private static Mutex mut = new Mutex();
        private static Network _net;
        private SmileSingleton()
        {
            _net = new Network("./network.xdsl");
        }

        public static SmileSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new SmileSingleton();
                    }
                }
                return instance;
            }
        }

        public void init()
        {
            Debug.WriteLine("READY");
        }

        public IOrderedEnumerable<KeyValuePair<String, double>> getValueForHandType(Table table, Player player, HandHistories.Objects.Cards.Street forceStreet = HandHistories.Objects.Cards.Street.Null)
        {
            IOrderedEnumerable<KeyValuePair<String, double>> value = null;
            lock (syncLock)
            {
                value = _net.getValueForHandType(table, player, forceStreet);
            }
            return value;
        }
    }
}
