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
        private static object locker = new Object();
        private static Mutex mut = new Mutex();
        private static Network _net;
        private SmileSingleton() 
        {
            _net = new Network("./network.xsdl");
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

        public Smile.Network cloneNetwork()
        {
            return (Smile.Network)_net.SmileNetwork.Clone();
        }

        public void init()
        {
            Debug.WriteLine("READY");
        }

        public IOrderedEnumerable<KeyValuePair<String, double>> getValueForHandType(Table table, Player player, HandHistories.Objects.Cards.Street forceStreet = HandHistories.Objects.Cards.Street.Null)
        {
            IOrderedEnumerable<KeyValuePair<String, double>> value = null;
            mut.WaitOne();
            value = instance.getValueForHandType(table, player, forceStreet);
            mut.ReleaseMutex();
            return value;
        }
    }
}
