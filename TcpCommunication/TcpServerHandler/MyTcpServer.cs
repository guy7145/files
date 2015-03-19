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
        private bool _running, _connected;
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
            _connected = _running = false;
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
            _run();
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
            finally
            {
                _unrun();
            }
        }
        public bool StreamOut(byte[] data)
        {
            _run();
            try
            {
                socket.Send(data);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                _unrun();
            }
        }

        public bool RequestStop()
        {
            try
            {
                socket.Close();
                _running = false;
                _connected = false;

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        private void _run()
        {
            _running = true;
        }
        private void _unrun()
        {
            _running = false;
        }
        public bool IsRunning()
        {
            return this._running;
        }
        public bool IsConnected()
        {
            return this._connected;
        }
    }
}
