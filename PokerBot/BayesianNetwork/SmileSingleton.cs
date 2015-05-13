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
        private Queue<List<Tuple<String, String>>> queueMessage;
        private List<AnonymousPipe.AnonymousPipeServer> listServer;
        private SmileSingleton()
        {

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

        public void init(int max)
        {
            for (int i = 0; i < max; i++)
            {
                listServer.Add(new AnonymousPipe.AnonymousPipeServer());
            }
        }

        public List<String> request(List<Tuple<String, String>> data)
        {
            AnonymousPipe.AnonymousPipeServer pipeServer = null;
            do
            {
                pipeServer = selectServer();
            } while (pipeServer == null);

            return pipeServer.request(data);
        }

        private AnonymousPipe.AnonymousPipeServer selectServer()
        {
            AnonymousPipe.AnonymousPipeServer pipeServer = null;
            lock (syncLock)
            {
                foreach (var server in listServer)
                {
                    if (server.Busy == false)
                    {
                        pipeServer = server;
                        pipeServer.Busy = true;
                    }
                }
            }
            return pipeServer;
        }
    }
}
