using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alten_Connected_Vehicles.VehiclesRobot
{
    public partial class frm_VehicleRobot : Form
    {
        /// <summary>
        /// Demo Robot 
        /// </summary>
        List<string> Packets;
        public frm_VehicleRobot()
        {
            InitializeComponent();
            PrepareData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendData();
        }


        private void SendData()
        {
            IPAddress ServerIP = IPAddress.Parse(txt_ServerIP.Text);
            int prt = Int32.Parse(textBox1.Text);
            IPEndPoint IPEP = new IPEndPoint(ServerIP, prt);

            TcpClient tc = new TcpClient();
            tc.Connect(IPEP);
            NetworkStream ns = tc.GetStream();
            Random rand = new Random();
            int RND = rand.Next(0, 14);
            byte[] MessageBytes = Encoding.ASCII.GetBytes(Packets[RND]);
           
            ns.Write(MessageBytes,0,MessageBytes.Length);
               
            
           
            ns.Close();
            tc.Close();

        }

        /// <summary>
        /// Prepare Data packets 
        /// </summary>
        private void PrepareData()
        {
            Packets = new List<string>();
            Packets.Add("$$GHI7891");
            Packets.Add("$$GHI7890");

            Packets.Add("$$DEF4561");
            Packets.Add("$$DEF4560");

            Packets.Add("$$STU9011");
            Packets.Add("$$STU9010");

            Packets.Add("$$PQR6781");
            Packets.Add("$$PQR6780");
            
            Packets.Add("$$MNO3451");
            Packets.Add("$$MNO3450");

            Packets.Add("$$JKL0121");
            Packets.Add("$$JKL0120");

            Packets.Add("$$ABC1231");
            Packets.Add("$$ABC1230");
        }
    }
}
