using Microsoft.AspNetCore.Mvc;
using Weather;
using System.Net.Mime;
using Weather.Controllers;

namespace weather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : Controller
    {
        private readonly ILogger<LocationController> _logger;

        public LocationController(ILogger<LocationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<Location[]> Find(int? location = null)
        {
            try
            {
                using(WeatherContext context = new WeatherContext()) 
                { 
                        Location[] locations;
                        if (location == null) 
                        {
                            locations = context.locations.ToArray();
                        }
                        else
                        {
                            locations = context.locations.Where(l => l.ID == location).ToArray();
                        }
                        return locations;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        [HttpPost]
        public IResult CreateLocation (string location)
        {
            try
            {
                using (WeatherContext ctx = new WeatherContext())
                {
                    Location? loc = ctx.locations.Where(l => l.Name == location).FirstOrDefault();
                    if (loc != null)
                    {
                        return Results.BadRequest();
                    }
                    else
                    {
                        loc = new Location() 
                        {
                            Name = location,
                        };
                        ctx.locations.Add(loc);
                        ctx.SaveChanges();
                        return Results.Ok();
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return Results.BadRequest();
            }
        }

    }
}
