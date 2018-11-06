using System.Web.Mvc;
using System.Web.Routing;

namespace HearstMappingsEditor
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*[/\\])?favicon\.((ico)|(png))(/.*)?" });

            routes.MapRoute(
                name: "ErrorDefault",
                url: "Error",
                defaults: new
                {
                    controller = "Error",
                    action = "Index"
                }
            );

            routes.MapRoute(
                name: "ErrorNotFound",
                url: "NotFoundPage",
                defaults: new
                {
                    controller = "Error",
                    action = "NotFoundPage"
                }
            );

            routes.MapRoute(
                name: "ErrorForbidden",
                url: "Forbidden",
                defaults: new
                {
                    controller = "Error",
                    action = "Forbidden"
                }
            );

            routes.MapRoute(
                name: "AccountMappingsIndex",
                url: "AccountMappings",
                defaults: new { controller = "AccountMappings", action = "Index"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{filter}",
                defaults: new { controller = "AccountMappings", action = "Index", filter = UrlParameter.Optional }
            );
        }
    }
}
