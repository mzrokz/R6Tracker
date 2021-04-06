﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace R6T.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ScraperSvc()
            };
            ServiceBase.Run(ServicesToRun);
            // var oScraper = new Scraper.Main();
            // oScraper.Start();
        }
    }
}
