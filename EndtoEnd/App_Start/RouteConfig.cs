using System.Web.Mvc;
using System.Web.Routing;

namespace EndtoEnd
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "SecuritiesMfById",
                url: "SecurityMfMvc/Edit/{id}",
                defaults: new { controller = "SecurityMfMvc", action = "Edit", id = "" },
                constraints: new { id = @"\d+" }
               );
            routes.MapRoute(
                name: "SecuritiesMfBySymbol",
                url: "SecurityMfMvc/Select/{id}",
                defaults: new { controller = "SecurityMfMvc", action = "Select", id = "" },
                constraints: new { id = @"\d+" }
               );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "SecurityMfMvc", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}