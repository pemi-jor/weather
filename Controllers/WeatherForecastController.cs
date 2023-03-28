using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Net.Mime;
using weather;

namespace Weather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<SWeather> GetWeathers(int? location = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (!startDate.HasValue && !endDate.HasValue)
            {
                startDate = DateTime.Now.AddDays(-7);
                endDate = DateTime.Now;
            }
            else if (!endDate.HasValue && startDate.HasValue)
            {
                endDate = startDate;
            }
            else if (!startDate.HasValue && endDate.HasValue)
            {
                startDate = endDate;
            }
            try
            {
                using (WeatherContext context = new WeatherContext())
                {
                
                    SWeather[] weathers;
                    if(location is not null)
                    {
                        weathers = context.weathers.Where
                            (w => w.Date >= startDate && w.Date <= endDate && w.LocationID == location).ToArray();
                    }
                    else {
                        weathers = context.weathers.ToArray();
                    }
                    return weathers;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Array.Empty<SWeather>();
                throw;
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IResult CreateWeather(SWeather weatherForecast)
        {
            try
            {
                using (WeatherContext context = new WeatherContext())
                {
                    SWeather? weather = context.weathers.
                        Where(w => w.Date == weatherForecast.Date && w.LocationID == weatherForecast.LocationID).FirstOrDefault();
                    if (weather != null) 
                    {
                        weather.TemperatureC = weatherForecast.TemperatureC;
                        context.weathers.Update(weather);
                        context.SaveChanges();
                    }
                    else
                    {
                        context.weathers.Add(weatherForecast);
                        context.SaveChanges();
                    }

                    return Results.Ok();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Results.BadRequest();
                throw;
            }
        }
    }
}