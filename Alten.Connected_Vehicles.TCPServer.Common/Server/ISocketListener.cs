using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.TCPServer.Common.Server
{
    interface ISocketListener
    {
        void StartSocketListener();

        void SocketDataReceiver();

        void AcceptClientData(byte[] bufferdata);

        bool AuthenticateClient();

        void StopSocketListener();

        bool IsMarkedForDeletion();

        
        
    }
}
