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
        private volatile bool _streamIn, _connected;
        private IPAddress serverIp;
        private int serverPort;
        private TcpClient client;
        private Stream stream;

        public MyTcpClient(IPAddress ip, int port)
        {
            this.serverIp = ip;
            this.serverPort = port;
            client = new TcpClient();
            _streamIn = _connected = false;
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
        }
        public void StreamIn()
        {
            ASCIIEncoding asciiEncoder = new ASCIIEncoding();
            byte[] byteResponseRecieved = new byte[256];
            int k;

            _streamIn = true;

            try
            {
                while (_streamIn)
                {
                    k = stream.Read(byteResponseRecieved, 0, 256);
                    char[] c = new char[byteResponseRecieved.Length];
                    for (int i = 0; i < k; i++)
                        c[i] = Convert.ToChar(byteResponseRecieved[i]);
                    Console.WriteLine((new string(c)).Split('\0')[0]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public bool RequestStop()
        {
            try
            {
                RequestStopStreamIn();
                RequestDisconnect();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool RequestStopStreamIn()
        {
            try
            {
                stream.Close();
                _streamIn = false;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool RequestDisconnect()
        {
            try
            {
                client.Close();
                _connected = false;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool IsStreaming()
        {
            return this._streamIn;
        }
        public bool IsConnected()
        {
            return this._connected;
        }
    }
}
