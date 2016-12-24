using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataCodeWindowsService
{
    public class Watcher
    {
        //for Log-file
        static FileStream log = new FileStream(@"C:\datacode.log", FileMode.Append, FileAccess.Write);
        static StreamWriter writer = new StreamWriter(log);

        //fields
        string url;
        double interval;

        public Watcher(string url, double interval)
        {
            this.url = url;
            this.interval = interval;
        }

        //method which provide creating Timer for particular instance of the class
        public System.Timers.Timer CreateTimer()
        {
            System.Timers.Timer timer = new System.Timers.Timer(interval);
            timer.Elapsed += (sender, e) => SiteRequestResponse(url);

            return timer;
        }

        public void SiteRequestResponse(string url)
        {
            string datetime = DateTime.Now.ToString("G");
            string state = null;

            WebRequest webRequest = WebRequest.Create(url);
            try
            {   //try get response from site
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    state = "is available.";
                }
            }
            catch (WebException)
            {
                state = "is not available.";
            }
            finally
            {
                //generate message to log-file
                writer.WriteLine($"{datetime} | {url} {state}");
                writer.Flush();
            }
        }

    }
}
