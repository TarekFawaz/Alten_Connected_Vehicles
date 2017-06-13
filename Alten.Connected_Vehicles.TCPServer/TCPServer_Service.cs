using Alten.Connected_Vehicles.Infrastructure.Logger;
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

namespace Alten.Connected_Vehicles.TCPService
{
    public partial class TCPServer_Service : ServiceBase
    {

        #region Member Data 
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private Container components = null;
        private TCPServer.Common.Server.TCPServer server = null;
        private string ServerIP = string.Empty;
        private string ServerPort = string.Empty;
        private Logger log = new Logger();




        #endregion

        #region CTOR
        public TCPServer_Service()
        {
            InitializeComponent();
        }
        #endregion 

        #region Additonal initailizers 
        

        /// <summary> 
		/// Remnoving the desginer method to create our own initalizer 
		/// </summary>
		private void InitializeComponent()
        {
            components = new Container();
            this.ServiceName = "TCPServer_Service";

            ServerIP = ConfigurationManager.AppSettings["ServerIP"];
            ServerPort = ConfigurationManager.AppSettings["ServerPort"];
            

        }

        #endregion

        #region Disposing 
        /// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion 

        protected override void OnStart(string[] args)
        {
            // Create the Server Object and Start it.
            if (!string.IsNullOrEmpty(ServerIP) && !string.IsNullOrEmpty(ServerPort))
            {
                server = new TCPServer.Common.Server.TCPServer(ServerIP, ServerPort);
                server.StartServer();
            }
            else
            {
                log.LogToFile("Fail Fast:Server IP or Port Not configured");
                Environment.FailFast("Server IP or Port Not configured");
            }
        }

        protected override void OnStop()
        {
            // Stop the Server. Release it.
            server.StopServer();
            server = null;
        }
    }
}
