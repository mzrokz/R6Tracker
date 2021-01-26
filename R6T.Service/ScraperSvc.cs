using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using PuppeteerSharp;

namespace R6T.Service
{
    public partial class ScraperSvc : ServiceBase
    {
        private Timer _timer;
        public ScraperSvc()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);

            _timer = new Timer();
            _timer.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["ServiceInterval"]);
            _timer.Enabled = true;
            _timer.Elapsed += new ElapsedEventHandler(OnTimerElapsed);
            _timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var oScraper = new Scraper.Main();
                oScraper.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }

        protected override void OnStop()
        {
            _timer.Enabled = false;
            _timer.Stop();
            _timer.Dispose();
        }
    }
}
