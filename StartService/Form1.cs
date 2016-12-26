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
        ServiceController service;
        string exeServiceFile;
        public ServiceForm()
        {
            InitializeComponent();
            exeServiceFile = GetPath();
            service = new ServiceController("=DataCode=");
            stopButton.Visible = false;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Install service
                ManagedInstallerClass.InstallHelper(new[] { exeServiceFile });
                service.Start();
                MessageBox.Show("The service is run");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                stopButton.Visible = true;
            }
        }
        private void stopButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Uninstall service
                ManagedInstallerClass.InstallHelper(new[] { "/u", exeServiceFile });
                MessageBox.Show("The service was uninstalled");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                stopButton.Visible = false;
                startButton.Visible = true;
            }
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
