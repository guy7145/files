using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace MyBot.ThreadObjects
{
    class MyTcpClient
    {
        private volatile bool _running, _connected;
        private string serverIp;
        private int serverPort;
        private TcpClient client;
        private Stream stream;
        private OperationWorker worker;

        public void Connect(string ip, int port)
        {
            try
            {
                client = new TcpClient();
                client.Connect(ip, port);
                stream = client.GetStream();
                _connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
        public void StreamOut()
        {
            _running = true;
            ASCIIEncoding asciiEncoder = new ASCIIEncoding();
            byte[] byteMsg;
            string msg;

            try
            {
                while (_running)
                {
                    msg = "id " + MyBot.id + "; status " + this.worker.GetStatus() + ";";
                    byteMsg = asciiEncoder.GetBytes(msg);
                    stream.Write(byteMsg, 0, byteMsg.Length);
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
        public void StreamIn()
        {
            _running = true;
            ASCIIEncoding asciiEncoder = new ASCIIEncoding();
            byte[] byteResponseRecieved;
            string responseReceived;
            int k;

            try
            {
                while (_running)
                {
                    byteResponseRecieved = new byte[100];
                    k = stream.Read(byteResponseRecieved, 0, 100);
                    char[] c = new char[byteResponseRecieved.Length];
                    for (int i = 0; i < k; i++)
                        c[i] = Convert.ToChar(byteResponseRecieved[i]);
                    responseReceived = (new string(c)).Split('\0')[0];
                    if (responseReceived == "abort")
                        RequestStop();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
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
        public bool IsRunning()
        {
            return this._running;
        }
        public bool IsConnected()
        {
            return this._connected;
        }
        public void SetWorkerObject(OperationWorker worker)
        {
            this.worker = worker;
        }
    }
}
