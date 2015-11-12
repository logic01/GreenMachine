using System.Web.Mvc;
using System.Web.Routing;
using GreenMachine.Web.App_Start;
using System.Web.Optimization;

namespace GreenMachine.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
