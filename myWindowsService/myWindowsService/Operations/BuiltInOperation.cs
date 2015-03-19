using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBot.Operations
{
    enum Methods { Abort, Sleep }
    class BuiltInOperation
    {
        public static void Abort()
        {

        }
        public static void Sleep(int miliseconds)
        {
            System.Threading.Thread.Sleep(miliseconds);
        }
        public static void Operate(string operation)
        {
            string[] name = operation.Split(' ');
            Methods method = (Methods)Enum.Parse(typeof(Methods), name[0]);
            switch (method)
            {
                case Methods.Sleep: Sleep(int.Parse(name[1]));
                    break;
                default: break;
            }
        }
    }
}
