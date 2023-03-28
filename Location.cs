using Microsoft.Extensions.Hosting;
using Weather;

namespace weather
{
    public class Location
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public List<SWeather>? Weathers { get; set; }
    }
}
