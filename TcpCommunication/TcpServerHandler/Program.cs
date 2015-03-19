using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            IPAddress ip = IPAddress.Parse(Console.ReadLine());
            Console.WriteLine("port: ");
            int port = int.Parse(Console.ReadLine());

            MyTcpClient cl = new MyTcpClient(ip, port);
        }
    }
}
