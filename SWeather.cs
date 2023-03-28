using weather;

namespace Weather
{
    public class SWeather
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }

        public double WindSpeed { get; set; }

        public int TemperatureC { get; set; }

        public int? LocationID { get; set; }

        public Location? Location { get; set; }
    }
}