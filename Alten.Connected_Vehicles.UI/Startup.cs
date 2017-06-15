using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Alten.Connected_Vehicles.UI.Startup))]
namespace Alten.Connected_Vehicles.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
