using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ControlPanel.ThreadObjects
{
    class MyTcpServer
    {
        private bool _running;
        private int port;
        private IPAddress ip;
        private TcpListener listener;
        private Socket socket;
        ASCIIEncoding asciiEncoder;
        private byte[] byte_a, byte_b;

        public MyTcpServer(string ip, int port)
        {
            this.ip = IPAddress.Parse(ip);
            this.port = port;
            listener = new TcpListener(this.ip, this.port);
        }

        public void Connect()
        {
            try
            {
                /* Start Listeneting at the specified port */
                listener.Start();

                Console.WriteLine("The server is running at port {0}...", port);
                Console.WriteLine("The local End point is: " + listener.LocalEndpoint);
                Console.WriteLine("Waiting for a connection.....");

                socket = listener.AcceptSocket();
                Console.WriteLine("Connection accepted from " + socket.RemoteEndPoint);
                asciiEncoder = new ASCIIEncoding();
                _running = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
        public void StreamIn()
        {
            try
            {   
                while (_running)
                {
                    byte_b = new byte[1000];
                    int k = socket.Receive(byte_b);
                    Console.WriteLine("Recieved...");
                    for (int i = 0; i < k; i++)
                        Console.Write(Convert.ToChar(byte_b[i]));
                    Console.WriteLine();
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
        public void Send(string msg)
        {
            byte_a = asciiEncoder.GetBytes(msg);
            socket.Send(byte_a);
            Console.WriteLine("Message Sent");
        }
        public bool RequestStop()
        {
            try
            {
                socket.Close();
                listener.Stop();
                _running = false;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    
    }
}
