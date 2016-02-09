using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Core.Interfaces;
using Core.Models;

namespace AutofacMVC
{
	public class AutofacConfig
	{
		public static void ConfigureContainer()
		{
			var builder = new ContainerBuilder();

			// Register dependencies in controllers
			builder.RegisterControllers(typeof(MvcApplication).Assembly);
			builder.RegisterSource(new ViewRegistrationSource());

			builder.RegisterType<SquareMatrixModel>().As<ISquareMatrixModel>().InstancePerRequest();

			var container = builder.Build();

			// Set MVC DI resolver to use our Autofac container
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}