using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;
using System;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using WebApi.Constraints;
using WebApi.ExceptionHandlers;
using WebApi.Filters;
using WebApi.Loggers;

[assembly: OwinStartup(typeof(WebApi.Startup))]
namespace WebApi
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Startup
    {
        public static HttpConfiguration HttpConfiguration { get; set; } = new HttpConfiguration();

        public void Configuration(IAppBuilder app)
        {
            var config = Startup.HttpConfiguration;

            var json = config.Formatters.JsonFormatter.SerializerSettings;
            json.ContractResolver = new CamelCasePropertyNamesContractResolver();
            json.Formatting = Formatting.Indented;

            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            ConfigureWebApi(app, config);
            ConfigureSwashbuckle(config);

        }

        private void ConfigureSwashbuckle(HttpConfiguration config)
        {
       
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "A title for your API");
                var xmlDocPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\bin\\WebApi_2.2.xml";
                c.IncludeXmlComments(xmlDocPath);
            })
            .EnableSwaggerUi();


        }

        private static void ConfigureWebApi(IAppBuilder app, HttpConfiguration config)
        {
            app.UseCors(CorsOptions.AllowAll);

            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("identity", typeof(IdConstraint));
            config.MapHttpAttributeRoutes(constraintResolver);

            config.Services.Replace(typeof(IExceptionLogger), new UnhandledExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new UnhandledExceptionHadler());

            config.Filters.Add(new DbUpdateExceptionFilterAttribute());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
