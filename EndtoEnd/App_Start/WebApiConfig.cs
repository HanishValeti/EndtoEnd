using System.Web.Http;

namespace EndtoEnd
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "ActionById",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = "GetById", id = RouteParameter.Optional },
                constraints: new { id = @"\d+" }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { id = @"\d+" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApiBySymbol",
                routeTemplate: "api/{controller}/{symbol}",
                defaults: new { symbol = RouteParameter.Optional }
            );
        }
    }
}
