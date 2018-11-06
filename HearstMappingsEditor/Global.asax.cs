using HearstMappingsEditor.App_Start;
using HearstMappingsEditor.Common;
using HearstMappingsEditor.Common.Interfaces;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HearstMappingsEditor
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Add(new CustomRazorViewEngine());
            SimpleInjectorInitializer.Initialize();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            if (exception != null)
            {
                (SimpleInjectorInitializer.Container?.GetInstance<ILogger>() ?? new Logger()).Error("An unexpected error occured", exception);
            }
        }
    }
}
