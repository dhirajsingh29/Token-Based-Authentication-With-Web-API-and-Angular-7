using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPIProject
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.EnableCors(
            //    new EnableCorsAttribute("http://localhost:4200",
            //        "*", "*"));

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
