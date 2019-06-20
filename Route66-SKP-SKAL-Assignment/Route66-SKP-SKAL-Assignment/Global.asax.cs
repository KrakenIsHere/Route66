using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace Route66_SKP_SKAL_Assignment
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}