using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using MyBot.Operations;

namespace MyBot.ThreadObjects
{
    class OperationWorker
    {
        private volatile bool _running;
        private string url;
        private int startId;
        private volatile int status;

        public OperationWorker(string url, int startId)
        {
            _running = true;
            this.url = url;
            this.startId = startId;
        }
        public void DoWork()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.url);
            
            XmlNode node;
            object obj = 0;
            OperationType type;
            bool singleLine;

            XmlNode _node = Actions.GetToStartPosition(doc, startId);

            while (_running)
            {
                //start again from the top
                status = startId;
                node = _node;

                while (node != null && node.Name == "script")
                {
                    type = (OperationType)int.Parse(node.Attributes["type"].Value);
                    singleLine = bool.Parse(node.Attributes["singleLine"].Value);
                    switch (type)
                    {
                        case OperationType.CMD:
                            if (singleLine) CmdOperation.Operate(node.InnerText, true);
                            else CmdOperation.OperateScript(node.Attributes["url"].Value, true);
                            break;
                        case OperationType.JavaScript:
                            if (singleLine) obj = JSOperation.Operate(node.InnerText, obj);
                            else obj = JSOperation.OperateScript(node.Attributes["url"].Value, obj);
                            break;
                        default: BuiltInOperation.Operate(node.InnerText);
                            break;
                    }
                    node = node.NextSibling;
                    status++;
                }
                System.Threading.Thread.Sleep(5000);
            }
        }
        public int GetStatus()
        {
            return this.status;
        }
        public bool RequestStop()
        {
            _running = false;
            return true;
        }
    }
}
