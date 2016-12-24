using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;

namespace DataCodeWindowsService
{
    public partial class Service : ServiceBase
    {
        //instances for each site
        Watcher googleWatcher;
        Watcher microsoftWatcher;
        Watcher appleWatcher;

        //timers which will be created for following
        Timer googleTimer;
        Timer microsoftTimer;
        Timer appleTimer;

        public Service()
        {
            InitializeComponent();
            CanPauseAndContinue = true;

            //owin
            WebApp.Start<Startup>("http://localhost:9000");

            //sites for watching 
            googleWatcher = new Watcher("http://google.com", 120000);
            microsoftWatcher = new Watcher("http://microsoft.com", 300000);
            appleWatcher = new Watcher("http://apple.com", 25000);

            //timers
            googleTimer = googleWatcher.CreateTimer();
            microsoftTimer = microsoftWatcher.CreateTimer();
            appleTimer = appleWatcher.CreateTimer();
        }
        //
        //
        //
        protected override void OnStart(string[] args)
        {
            StatusController.State = true;

            googleTimer.Start();
            microsoftTimer.Start();
            appleTimer.Start();
        }
        protected override void OnStop()
        {
            StatusController.State = false;

            googleTimer.Stop();
            microsoftTimer.Stop();
            appleTimer.Stop();
        }
        protected override void OnPause()
        {
            StatusController.State = false;

            googleTimer.Stop();
            microsoftTimer.Stop();
            appleTimer.Stop();
        }
        protected override void OnContinue()
        {
            StatusController.State = true;

            googleTimer.Start();
            microsoftTimer.Start();
            appleTimer.Start();
        }
    }
}
