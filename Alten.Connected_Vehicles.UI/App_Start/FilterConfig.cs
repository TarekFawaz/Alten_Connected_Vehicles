using System.Web;
using System.Web.Mvc;

namespace Alten.Connected_Vehicles.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
