using Newtonsoft.Json;
using System.Web.Http.ExceptionHandling;
using WebApi.HttpActionResults;

namespace WebApi.ExceptionHandlers
{
    public class UnhandledExceptionHadler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
#if DEBUG
            var content = JsonConvert.SerializeObject(context.Exception);
#else
            var content = @"{ ""message"" : ""Oops, something unexpectedly went wrong"" }";
#endif      
            context.Result = new ErrorContentResult(content, "application/json", context.Request);
        }
    }
}