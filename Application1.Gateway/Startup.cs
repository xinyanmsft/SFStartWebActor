using System.Web.Http;
using Owin;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using System.Fabric;

namespace Application1.Gateway
{
    public static class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public static void ConfigureApp(IAppBuilder appBuilder, ServiceContext serviceContext)
        {
            appBuilder.Use(typeof(ServiceMiddleware));

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Filters.Add(new GatewayExceptionFilterAttribute(serviceContext));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);

            appBuilder.UseFileServer(new FileServerOptions()
            {
                RequestPath = PathString.Empty,
                FileSystem = new PhysicalFileSystem(@".\Contents")
            });
        }
    }
}
