using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBot.Operations
{
    class CmdOperation
    {
        public static void Operate(string command)
        {
            Operate(command, true);
        }
        public static void Operate(string command, bool hidden)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            if (hidden)
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + command;
            process.StartInfo = startInfo;
            process.Start();
        }
        public static void OperateScript(string url)
        {
            OperateScript(url, true);
        }
        public static void OperateScript(string url, bool hidden)
        {
            string[] script = Actions.ReadFile(url).Split('\n');
            if (script[0].Length > 2)
            {
                Operate(script[0]);
            }
            for (int i = 1; i < script.Length; i++)
                if (script[i].Length > 2)
                {
                    script[i] = script[i].Substring(0, script[i].Length - 2);
                    Operate(script[i], hidden);
                }
        }
    }
}
