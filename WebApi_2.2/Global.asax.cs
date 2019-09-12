using System.Web;
using System.Web.Mvc;

namespace WebApi._2
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
        }
    }
}
