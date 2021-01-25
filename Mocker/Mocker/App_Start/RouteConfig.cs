using System.Web.Mvc;
using System.Web.Routing;

namespace Mocker
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Conventional Routing
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}"
            );

            routes.IgnoreRoute("{controller}/{action}/{id}");
        }
    }
}
