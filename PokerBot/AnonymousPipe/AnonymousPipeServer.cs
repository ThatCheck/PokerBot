using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.AnonymousPipe
{
    public class AnonymousPipeServer
    {
        public bool Busy { get; set; }
        public NamedPipeServerStream PipeServer { get; set; }
        public String Name { get; set; }
        public StreamWriter sw { get; set; }
        public StreamReader sr { get; set; }
        public AnonymousPipeServer()
        {
            Guid g = Guid.NewGuid();
            Busy = false;
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            this.Name = GuidString;
            this.startServer();
        }

        public void startServer()
        {
            Process pipeClient = new Process();

            pipeClient.StartInfo.FileName = "./BaysianNetworkRepartitor.exe";
            pipeClient.StartInfo.WorkingDirectory = Environment.CurrentDirectory + '\\';
            NamedPipeServerStream PipeServer = new NamedPipeServerStream(this.Name, PipeDirection.InOut, 1, PipeTransmissionMode.Message);
            Console.WriteLine("[SERVER] Current TransmissionMode: {0}.", PipeServer.TransmissionMode);

            // Pass the client process a handle to the server.
            pipeClient.StartInfo.Arguments = this.Name +" ./network.xdsl";
            pipeClient.StartInfo.UseShellExecute = false;
            pipeClient.Start();
            PipeServer.WaitForConnection();
            sw = new StreamWriter(PipeServer);
            sr = new StreamReader(PipeServer);
            Console.WriteLine("[SERVER] PIPE CONNECTED.", PipeServer.TransmissionMode);
        }

        public List<String> request(List<Tuple<String, String>> list)
        {
            try
            {
                Busy = true;
                List<String> listReturn = new List<String>();
                foreach (var tuple in list)
                {
                    sw.WriteLine(tuple.Item1 + ";" + tuple.Item2);
                }
                sw.WriteLine("END");
                sw.Flush();
                //PipeServer.WaitForPipeDrain();
                string temp;
                do
                {
                    temp = sr.ReadLine();
                    if (!temp.StartsWith("END"))
                    {
                        listReturn.Add(temp);
                    }
                }
                while (!temp.StartsWith("END"));
                return listReturn;
            }
            // Catch the IOException that is raised if the pipe is broken
            // or disconnected.
            catch (Exception e)
            {
                Console.WriteLine("[SERVER] Error: {0}", e.Message);
            }
            finally
            {
                Busy = false;
            }
            return null;
        }
    }
}
