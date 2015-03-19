using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ControlPanel.ThreadObjects;

namespace ControlPanel
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread tcpThread;
            bool _running = true;
            string msg;

            Console.WriteLine("Enter your ip address: ");
            string ip = Console.ReadLine();
            Console.WriteLine("Enter port: ");
            int port = int.Parse(Console.ReadLine());
            MyTcpServer server = new MyTcpServer(ip, port);
            server.Connect();
            tcpThread = new Thread(server.StreamIn);
            tcpThread.Start();

            while (_running)
            {
                server.Send(msg = Console.ReadLine());
                if (msg == "abort")
                {
                    if (server.RequestStop())
                        _running = false;
                    else
                        Console.WriteLine("failed to abort");
                }
            }
            Console.ReadLine();
        }
    }
}
