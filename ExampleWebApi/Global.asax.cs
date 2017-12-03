using System.Web.Http;
using System.Web.Mvc;
using ExampleWebApi.Infrastructure;

namespace ExampleWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DependencyInjector.Initialize();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
