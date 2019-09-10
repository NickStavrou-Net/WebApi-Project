using System.Web.Http;
using WebApi_2._2.Dtos;

namespace WebApi.Controllers
{
    public class AuthorizeController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post(AuthorizeRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }
    }
}
