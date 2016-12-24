using System;
using System.Reflection;
using System.Windows.Forms;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace StartService
{
    public partial class ServiceForm : Form
    {
        string ServiceExeFile;
        ServiceController service;
        string exeServiceFile;
        public ServiceForm()
        {
            InitializeComponent();
            //take path to this assembly, in further will redefine for right path which contain service
            ServiceExeFile = Assembly.GetExecutingAssembly().Location;

            //Will use for launch service
            service = new ServiceController("=DataCode=");

            //get right path to service-file .exe
            exeServiceFile = GetPath();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new[] { exeServiceFile });
                service.Start();
                MessageBox.Show("The service is run");
            }
            catch
            {
                MessageBox.Show("The service running already");
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            //Was created async method because ManagedInstallerClass.InstallHelper method destroy programm work few seconds
            UninstallServiceAsync();
        }

        async void UninstallServiceAsync()
        {

            await Task.Run(() =>
            {
                try
                {
                    //Latency is here, therefore was created async method
                    ManagedInstallerClass.InstallHelper(new[] { "/u", exeServiceFile });
                    MessageBox.Show("The service was uninstalled");
                }
                catch
                {
                    MessageBox.Show("The service is uninstalled already");
                }
            });
        }
        string GetPath()
        {
            //relate path to service
            string servicePath = @"DataCodeWindowsService\bin\Debug\DataCodeWindowsService.exe";

            int starterServicePathLength = @"StartService\bin\Debug\StartService.exe".Length;

            string newPath = null;

            //generate new path and then concate relate path to service
            for (int i = 0; i < Assembly.GetExecutingAssembly().Location.Length - starterServicePathLength; i++)
                newPath += Assembly.GetExecutingAssembly().Location[i];

            newPath += servicePath;

            return newPath;
        }
    }
}
