using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MyBot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            bool debug = true;
            if (debug)
            {
                MyBot service = new MyBot();
                service.OnDebug();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                new MyBot() 
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
