using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(Alten.Connected_Vehicles.TCPServer.Common.SignalR.Startup))]
namespace Alten.Connected_Vehicles.TCPServer.Common.SignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
