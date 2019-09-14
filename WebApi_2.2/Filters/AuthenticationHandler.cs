using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;


namespace WebApi.Filters
{
    public class AuthenticationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage message,
            CancellationToken token)
        {
            HttpContext.Current.User = new GenericPrincipal(
                //Hardcoded value
                new GenericIdentity("username"),
                //empty array string of roles
                new string[] { });

            return base.SendAsync(message, token);
        }
    }
}