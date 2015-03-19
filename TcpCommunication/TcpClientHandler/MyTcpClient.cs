using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TcpCommunication.TcpClientHandler
{
    public class MyTcpClient
    {
        private volatile bool _running, _connected;
        private IPAddress serverIp;
        private int serverPort;
        private TcpClient client;
        private Stream stream;

        public MyTcpClient(IPAddress ip, int port)
        {
            this.serverIp = ip;
            this.serverPort = port;
            client = new TcpClient();
        }
        public void Connect()
        {
            try
            {
                client.Connect(serverIp, serverPort);
                stream = client.GetStream();
                _connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
        public void StreamOut(string msg)
        {
            _run();
            ASCIIEncoding asciiEncoder = new ASCIIEncoding();
            byte[] byteMsg;

            try
            {
                byteMsg = asciiEncoder.GetBytes(msg);
                stream.Write(byteMsg, 0, byteMsg.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
            finally
            {
                _unrun();
            }
        }
        public string StreamIn()
        {
            _run();
            ASCIIEncoding asciiEncoder = new ASCIIEncoding();
            byte[] byteResponseRecieved;
            int k;

            try
            {
                byteResponseRecieved = new byte[100];
                k = stream.Read(byteResponseRecieved, 0, 100);
                char[] c = new char[byteResponseRecieved.Length];
                for (int i = 0; i < k; i++)
                    c[i] = Convert.ToChar(byteResponseRecieved[i]);
                return (new string(c)).Split('\0')[0];
            }
            catch (Exception e)
            {
                return e.Message;
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
                client.Close();
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
