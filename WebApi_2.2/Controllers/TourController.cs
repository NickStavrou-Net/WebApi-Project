using System.Linq;
using System.Web.Http;
using WebApi.DataAccess;
using WebApi_2._2.Dtos;

namespace WebApi.Controllers
{
    [RoutePrefix("api/tour")]
    public class TourController : ApiController
    {
        private AppDataContext context = new AppDataContext();

        [HttpGet]
        public IHttpActionResult GetAllTours([FromUri]bool freeOnly = false)
        {
            var query = context.Tours
                .Select(t => new TourDto
                {
                    Description = t.Description,
                    Name = t.Name,
                    Price = t.Price,
                    TourId = t.TourId
                })
                .AsQueryable();

            if (freeOnly) query = query.Where(i => i.Price == 0.0m);

            return Ok(query.ToList());
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetTourById(int? id)
        {
            if (id == null)
                return NotFound();

            var query = context.Tours
                .Where(t => t.TourId == id)
                .FirstOrDefault();

            return Ok(query);
        }

        [Route("{name}")]
        [HttpGet]
        public IHttpActionResult GetTourByName(string name)
        {
            var query = context.Tours
                .Where(t => t.Name.Contains(name))
                .AsQueryable();

            return Ok(query.ToList());
        }

        [HttpPost]
        public IHttpActionResult SearchTour([FromBody]TourSearchRequestDto request)
        {
            if (request.MinPrice > request.MaxPrice)
                return BadRequest("MinPrice must be less than MaxPrice");

            var query = context.Tours.AsQueryable();

            query = query.Where(i => i.Price <= request.MaxPrice
                          && i.Price >= request.MinPrice);

            return Ok(query.ToList());
        }
    }
}
