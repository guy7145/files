using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using TcpCommunication.TcpClientHandler;

namespace TcpCommunication.TcpServerHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ip: ");
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            Console.WriteLine("port: ");
            int port = 8001;

            
            MyTcpServer sr = new MyTcpServer(ip, port);
            MyTcpClient cl = new MyTcpClient(ip, port);
            Thread lstn = new Thread(sr.Listen);
            Thread rec = new Thread(cl.StreamIn);

            lstn.Start();
            cl.Connect();
            rec.Start();
            cl.StreamOut("abcdefg");
            Console.ReadLine();
        }
    }
}
