using CustomMediaTypeFormatterWebAPI_Demo.CustomMediaTypeFormatter;
using System.Web.Http;

namespace CustomMediaTypeFormatterWebAPI_Demo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services


            //config.Formatters.Add(new CustomXmlFormatter());
            //config.Formatters.Add(new CustomCSVFormatter());

            config.Formatters.Add(new CustomJsonFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
