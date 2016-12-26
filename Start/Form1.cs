using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Configuration.Install;

namespace Start
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Visible = true;
            button2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = true;

            ManagedInstallerClass.InstallHelper(new[] { exeServiceFile });
            service.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button2.Visible = false;
        }
    }
}
