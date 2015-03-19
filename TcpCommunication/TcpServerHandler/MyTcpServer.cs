using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TcpCommunication.TcpServerHandler
{
    public class MyTcpServer
    {
        private bool _connected, _listen, _streamIn;
        private int port;
        private IPAddress ip;
        private TcpListener listener;
        private Socket socket;
        ASCIIEncoding asciiEncoder;
        private byte[] byte_a, byte_b;

        public MyTcpServer(IPAddress ip, int port)
        {
            this.ip = ip;
            this.port = port;
            listener = new TcpListener(this.ip, this.port);
            asciiEncoder = new ASCIIEncoding();
            _connected = _listen = _streamIn = false;
        }
        public void Connect()
        {
            try
            {
                /* Start Listeneting at the specified port */
                listener.Start();
                while (_listen)
                {
                    Console.WriteLine("The server is running at port {0}...", port);
                    Console.WriteLine("The local End point is: " + listener.LocalEndpoint);
                    Console.WriteLine("Waiting for a connection.....");

                    socket = listener.AcceptSocket();
                    Console.WriteLine("Connection accepted from " + socket.RemoteEndPoint);
                }
                _connected = true;
            }
            catch (Exception e)
            {
                _connected = false;
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
        public byte[] StreamIn()
        {
            try
            {
                byte_b = new byte[256];
                int k = socket.Receive(byte_b);
                return byte_b;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
                return null;
            }
        }
        public bool StreamOut(byte[] data)
        {
            try
            {
                socket.Send(data);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public void RequestStopStreamIn()
        {
            _streamIn = false;
        }
        public bool RequestDisconnect()
        {
            try
            {
                socket.Close();
                _connected = false;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public void RequestStopListening()
        {
            this._listen = false;
        }
        public bool RequestKill()
        {
            bool ans = RequestDisconnect();
            if (ans)
            {
                RequestStopListening();
                RequestStopStreamIn();
            }
            return ans;
        }
        public bool IsConnected()
        {
            return this._connected;
        }
        public bool IsStreaming()
        {
            return this._streamIn;
        }
        public bool IsListening()
        {
            return this._listen;
        }
    }
}
