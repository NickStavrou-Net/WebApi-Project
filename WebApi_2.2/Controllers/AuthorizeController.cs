using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApi.DataAccess;
using WebApi.Dtos;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    public class AuthorizeController : ApiController
    {
        private readonly AppDataContext _db = new AppDataContext();
        private readonly JwtTokenHelper _tokenHelper = new JwtTokenHelper();

        public List<AppDto> Get()
        {
            return _db.AuthorizedApps
                    .Select(a => new AppDto
                    {
                        Name = a.Name,
                        TokenExpiration = a.TokenExpiration
                    }).ToList();
        }

        public IHttpActionResult Post(AuthorizeRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authApp = _db.AuthorizedApps
                .FirstOrDefault(i => i.AppToken == request.AppToken
                                     && i.AppSecret == request.AppSecret
                                     && DateTime.UtcNow < i.TokenExpiration);

            if (authApp == null)
                return Unauthorized();

            var token = _tokenHelper.CreateToken(authApp);

            return Ok(token);
        }
    }
}
