using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noesis.Javascript;
using System.Net;
using System.IO;
using System.Xml;

namespace MyBot
{
    class Actions
    {
        public static dynamic CheckIn()
        {
            var details = new { id = 0, status = 0, sleeptime = 3000, url = "http://daniellitech.com/GuyBot/Orders.xml", serverIp = "127.0.0.1", serverPort = 8001 };
            return details;
        }
        public static void CheckOut(int id)
        { 
        }
        public static string ReadFile(string url)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(url);
            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadToEnd();
            return content;
        }    
        public static XmlNode GetToStartPosition(XmlDocument doc, int startPos)
        {
            XmlNode tmp = doc.SelectNodes("/commands/script").Item(0);
            while (tmp != null && (tmp.Attributes == null || tmp.Attributes["id"] == null))
                tmp = tmp.NextSibling;
            while (tmp != null && startPos > int.Parse(tmp.Attributes["id"].Value))
                tmp = tmp.NextSibling;
            return tmp;
        }
    }
}