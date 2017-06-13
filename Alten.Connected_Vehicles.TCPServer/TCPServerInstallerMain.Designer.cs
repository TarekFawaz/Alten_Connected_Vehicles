namespace Alten.Connected_Vehicles.TCPService
{
    partial class TCPServerInstallerMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TCPServerProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.TCPServerInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // TCPServerProcessInstaller
            // 
            this.TCPServerProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.TCPServerProcessInstaller.Password = null;
            this.TCPServerProcessInstaller.Username = null;
            // 
            // TCPServerInstaller
            // 
            this.TCPServerInstaller.ServiceName = "TCPServer_Service";
            this.TCPServerInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // TCPServerInstallerMain
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.TCPServerProcessInstaller,
            this.TCPServerInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller TCPServerProcessInstaller;
        private System.ServiceProcess.ServiceInstaller TCPServerInstaller;
    }
}