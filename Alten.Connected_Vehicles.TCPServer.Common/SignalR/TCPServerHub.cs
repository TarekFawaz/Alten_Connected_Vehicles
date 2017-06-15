using Alten.Connected_Vehicles.Infrastructure.Logger;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.TCPServer.Common.SignalR
{
    [HubName("TCPServerHub")]
    public class TCPServerHub : Hub
    {

        public void NotifyStatus(string RegNo, bool status)
        {
            Clients.All.notifyStatus(RegNo, status);
        }
    }
}
