using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

namespace AutofacMVC
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AutofacConfig.ConfigureContainer();
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}
