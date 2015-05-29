using Smile;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BaysianNetworkRepartitor
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", args[0], PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation))
                {
                    pipeClient.Connect();
                    pipeClient.ReadMode = PipeTransmissionMode.Message;
                    Console.WriteLine("[CLIENT] Current TransmissionMode: {0}.", pipeClient.TransmissionMode);
                    Console.WriteLine("[CLIENT] Connected To {1} : {0}.", pipeClient.IsConnected,args[0]);
                    StreamReader sr = new StreamReader(pipeClient);
                    StreamWriter sw = new StreamWriter(pipeClient);
                    string nameBayesianNetworkToLoad = args[1];
                    Console.WriteLine("[Bayesian Network Client] Init network");
                    SmileNetwork.SmileNetwork network = new SmileNetwork.SmileNetwork(nameBayesianNetworkToLoad);
                    Console.WriteLine("[Bayesian Network Client] End init network");
                    do
                    {
                        try
                        {
                            List<Tuple<String, String>> list = new List<Tuple<string, string>>();
                            string temp = "";
                            do
                            {
                                temp = sr.ReadLine();
                                if (temp == null)
                                    temp = "";
                                //Console.WriteLine("Receive from {0} " + args[0]);
                                if (!temp.StartsWith("END") && temp != "")
                                {
                                    list.Add(Tuple.Create<String, String>(temp.Split(';')[0], temp.Split(';')[1]));
                                }
                            } while (!temp.StartsWith("END"));
                            var dataReturn = network.request(list);
                            foreach (var data in dataReturn)
                            {
                                sw.WriteLine(data);
                            }
                            sw.WriteLine("END");
                            sw.Flush();
                            pipeClient.WaitForPipeDrain();
                        }catch(Exception ex)
                        {
                            Console.WriteLine("Exception raise from {0} : {1} ", args[0], ex.Message);
                        }
                    } while (true);
                }
            }
        }
    }
}
