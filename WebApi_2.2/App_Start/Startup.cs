using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System.Web.Http;
using System.Web.Http.Routing;
using WebApi_2._2.Constraints;

[assembly: OwinStartup(typeof(WebApi.Startup))]
namespace WebApi
{
    public class Startup
    {
        public static HttpConfiguration HttpConfiguration { get; set; } = new HttpConfiguration();

        public void Configuration(IAppBuilder app)
        {
            var config = Startup.HttpConfiguration;
            ConfigureWebApi(app, config);

        }

        private static void ConfigureWebApi(IAppBuilder app, HttpConfiguration config)
        {
            app.UseCors(CorsOptions.AllowAll);

            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("identity", typeof(IdConstraint));

            config.MapHttpAttributeRoutes(constraintResolver);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { id = @"\d+" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultNameApi",
                routeTemplate: "api/{controller}/{name}",
                defaults: new { name = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }
    }
}
