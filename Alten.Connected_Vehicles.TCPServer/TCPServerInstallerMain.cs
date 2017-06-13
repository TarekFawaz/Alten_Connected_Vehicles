using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.TCPService
{
    [RunInstaller(true)]
    public partial class TCPServerInstallerMain : System.Configuration.Install.Installer
    {
        public TCPServerInstallerMain()
        {
            InitializeComponent();
        }
    }
}
