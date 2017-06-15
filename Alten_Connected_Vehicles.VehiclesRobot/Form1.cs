using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alten_Connected_Vehicles.VehiclesRobot
{
    public partial class frm_VehicleRobot : Form
    {
        /// <summary>
        /// Demo Robot 
        /// </summary>
        #region Member Data
        List<string> Packets;
        Socket socketSender;
        #endregion

        #region Delegates for sake of long run process (send) data run in another thread
        delegate void SetButtonEnabledCallback(Control ctrl,bool value);

        #endregion


        #region Delegate Handler 
        private void SetButtonEnabled(Control ctrl, bool value)
        {
            if (ctrl.InvokeRequired)
            {
                SetButtonEnabledCallback c = new SetButtonEnabledCallback(SetButtonEnabled);
                this.Invoke(c, new object[] { ctrl, value });
            }
            else
            {
                ctrl.Enabled = value;
            }
        }
        #endregion

        #region Form Methods 
        public frm_VehicleRobot()
        {
            InitializeComponent();
            PrepareData();
        }

       


        

        /// <summary>
        /// Prepare Data packets 
        /// </summary>
        private void PrepareData()
        {
            Packets = new List<string>();
            

            Packets.Add("$$DEF4560");
            Packets.Add("$$DEF4561");

            Packets.Add("$$STU9011");
            Packets.Add("$$STU9010");

            Packets.Add("$$PQR6781");
            Packets.Add("$$PQR6780");
            
            Packets.Add("$$MNO3450");
            Packets.Add("$$MNO3451");

            Packets.Add("$$JKL0121");
            Packets.Add("$$JKL0120");

            Packets.Add("$$GHI7890");
            Packets.Add("$$GHI7891");

            Packets.Add("$$ABC1231");
            Packets.Add("$$ABC1230");
        }

       

        private void btn_Start_Click(object sender, EventArgs e)
        {
           
            try
            {
                // Establish the remote end point for the socket 

                IPAddress ipAddr = IPAddress.Parse(txt_ServerIP.Text);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, int.Parse(txt_port.Text));

                socketSender = new Socket(AddressFamily.InterNetwork,
                                          SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint 
               
                socketSender.Connect(ipEndPoint);
                               
                if (socketSender.Connected)
                {
                    btn_Start.Enabled = false;
                    btn_Stop.Enabled = true;

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: {0}", ex.ToString());
            }
        }
        /// <summary>
        /// Button send handelr 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Send_Click(object sender, EventArgs e)
        {
            Thread sendThread = new Thread(SendData);
            sendThread.Start();
        }
        /// <summary>
        /// Stop button handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Stop_Click(object sender, EventArgs e)
        {
            socketSender.Shutdown(SocketShutdown.Both);
            socketSender.Close();
            btn_Send.Enabled = true;
        }
        /// <summary>
        /// Send bulk of data till the server refuse the connection 
        /// </summary>
        private void SendData()
        {
           SetButtonEnabled(btn_Send,false);
            int idx = 0;
            int xx = 0;
            while (xx<1025)
            {

                if(idx==14)
                {
                    idx = 0;
                }
                string theMessage = Packets[idx];

                byte[] msg = Encoding.ASCII.GetBytes(theMessage);

                // Send the data through the socket 
                int bytesSent = socketSender.Send(msg, SocketFlags.None);
                idx++;
                xx++;
                Thread.Sleep(1000);
            }

            socketSender.Shutdown(SocketShutdown.Both);
            socketSender.Close();
            SetButtonEnabled(btn_Send, true);
        }
        #endregion
    }
}
