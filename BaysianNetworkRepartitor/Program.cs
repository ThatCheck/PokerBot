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
                    Console.WriteLine("[CLIENT] Current TransmissionMode: {0}.",pipeClient.TransmissionMode);

                    string nameBayesianNetworkToLoad = args[1];
                    Console.WriteLine("[Bayesian Network Client] Init network");
                    SmileNetwork.SmileNetwork network = new SmileNetwork.SmileNetwork(nameBayesianNetworkToLoad);
                    Console.WriteLine("[Bayesian Network Client] End init network");
                    do
                    {
                        List<Tuple<String, String>> list = new List<Tuple<string, string>>();
                        using (StreamReader sr = new StreamReader(pipeClient))
                        {
                            string temp;
                            do
                            {
                                temp = sr.ReadLine();
                                if (!temp.StartsWith("END"))
                                {
                                    list.Add(Tuple.Create<String, String>(temp.Split(';')[0], temp.Split(';')[1]));
                                }
                            }
                            while (!temp.StartsWith("END"));
                        }
                        var dataReturn = network.request(list);
                        using (StreamWriter sw = new StreamWriter(pipeClient))
                        {
                            sw.AutoFlush = true;
                            foreach (var data in dataReturn)
                            {
                                sw.WriteLine(data);
                            }
                            sw.WriteLine("END");
                        }
                    } while (true);
                }
            }
        }
    }
}
