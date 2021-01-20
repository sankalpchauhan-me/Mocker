using DBLib.Adapter;
using DBLib.AppDBContext;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mocker
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Global Configuration
            GlobalConfiguration.Configure(WebApiConfig.Register);
            // Filter Registration
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            FilterConfig.RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters);
            // Route Registration
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Database Dropped each time a modification occurs in any model (Migration?)
            if (System.Configuration.ConfigurationManager.AppSettings["env"].Equals("dev"))
                Database.SetInitializer(new SampleDataSeeder());


        }
    }
}
