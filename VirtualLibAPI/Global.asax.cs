using Autofac;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VirtualLibrarity;
using VirtualLibrarity.Database;

namespace VirtualLibAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static IContainer container;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
            DatabaseConnector.DatabaseConnectionInit();
        }
    }
}
