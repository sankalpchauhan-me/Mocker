using DBLib.AppDBContext;
using Microsoft.Owin;
using Owin;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

[assembly: OwinStartup(typeof(Mocker.Startup))]

namespace Mocker
{
    public class Startup : HttpApplication
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            var config = CreateHttpConfiguration();
            //It configures ASP.NET Web API to run on top of OWIN
            app.UseWebApi(config);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters);

            //Database Dropped each time a modification occurs in any model (Migration?)
            if (System.Configuration.ConfigurationManager.AppSettings["env"].Equals("dev"))
                Database.SetInitializer(new SampleDataSeeder());
            else if (System.Configuration.ConfigurationManager.AppSettings["env"].Equals("test"))
                Database.SetInitializer(new DropCreateDatabaseAlways<MockSQLContext>());

        }

        public static HttpConfiguration CreateHttpConfiguration()
        {
            var httpConfiguration = new HttpConfiguration();
            //Enables Attribute Routing
            httpConfiguration.MapHttpAttributeRoutes();

            return httpConfiguration;
        }


    }
}
