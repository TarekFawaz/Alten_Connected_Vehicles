using Alten.Connected_Vehicles.Infrastructure.Logger;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.TCPServer.Common.SignalR
{
    public class TCPServerHub : Hub
    {

        #region Memnber Data 
        private static Dictionary<string, string> Units_Connection;
        private static Logger log = new Logger();
        #endregion


        #region Basic SignalR Server Hub methods
        /// <summary>
        /// Register Connected client 
        /// </summary>
        /// <param name="ConnectionId"></param>
        /// <param name="UnitRegNo"></param>
        public static void AddDevice(string ConnectionId, string UnitRegNo)
        {
            if (Units_Connection == null)
                Units_Connection = new Dictionary<string, string>();

            if (Units_Connection.ContainsKey(ConnectionId))
            {
                Units_Connection.Remove(ConnectionId);
            }

            Units_Connection.Add(ConnectionId, UnitRegNo);

        }
        /// <summary>
        /// Delete devices when connection obslete 
        /// </summary>
        /// <param name="ConnectionId"></param>
        public static void DeleteDevice(string ConnectionId)
        {
            try
            {
                if (Units_Connection != null)
                {
                    if (Units_Connection.ContainsKey(ConnectionId))
                    {
                        Units_Connection.Remove(ConnectionId);
                    }

                }
            }
            catch (Exception e)
            {
                log.LogToFile(string.Format("Server SignalR HUB : Deleteing Device from hub SignalR EX : {0}", e.Message));
            }
        }
        /// <summary>
        /// reuse same connection if exist 
        /// </summary>
        /// <param name="UnitRegNo"></param>
        /// <returns></returns>
        public static string GetConnection(string UnitRegNo)
        {
            try
            {
                if (Units_Connection == null)
                {
                    return string.Empty;
                }
                if (Units_Connection.ContainsValue(UnitRegNo))
                {
                    return Units_Connection.FirstOrDefault(c => c.Value == UnitRegNo).Key;
                }
                return string.Empty;

            }
            catch (Exception e)
            {
                log.LogToFile(string.Format("GetConnection from hub SignalR EX : {0}", e.Message));
                return string.Empty;
            }

        }

        /// <summary>
        /// handle RegNo change on same connection 
        /// </summary>
        /// <param name="UnitRegNo"></param>
        public void ChangeDeviceId(string UnitRegNo)
        {
            AddDevice(Context.ConnectionId, UnitRegNo);
            log.LogToFile(string.Format("Connection {0} changed Device to {1}", Context.ConnectionId, UnitRegNo));
        }
        #endregion

        #region Hub Behavior update 
        public override Task OnConnected()
        {
            try
            {
                GlobalHost.Configuration.TransportConnectTimeout = TimeSpan.FromSeconds(15);
                var DeviceId = Context.QueryString["UnitRegNo"];
                AddDevice(Context.ConnectionId, DeviceId);
            }
            catch (Exception e)
            {
                log.LogToFile(string.Format("Exception when device Connected  ex : {0}", e.Message));
            }


            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            try
            {
                DeleteDevice(Context.ConnectionId);
            }
            catch (Exception e)
            {
                log.LogToFile(string.Format("Exception when device Connected  ex : {0}", e.Message));
            }

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            try
            {
                GlobalHost.Configuration.TransportConnectTimeout = TimeSpan.FromSeconds(15);
                var DeviceId = Context.QueryString["UnitRegNo"];
                AddDevice(Context.ConnectionId, DeviceId);

            }
            catch (Exception e)
            {
                log.LogToFile(string.Format("Exception when device Connected  ex : {0}", e.Message));
            }
            return base.OnReconnected();
        }
        #endregion 
    }
}
