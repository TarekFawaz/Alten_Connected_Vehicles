using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.TCPServer.Common.Server
{
    /// <summary>
    /// Providing a protocol for impelmenting Any TCP server
    /// </summary>
    interface IServer
    {
        void Init(IPEndPoint ipNport);

        void StartServer();

        void StopServer();

        void StopAllSocketListers();

        void ServerWaitClient();

        void PurgingThreadStart();


    }
}
