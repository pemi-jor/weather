using Microsoft.EntityFrameworkCore;
using weather;

namespace Weather
{
    public class WeatherContext : DbContext 
    {
        public DbSet<SWeather> weathers { get; set; }
        public DbSet<Location> locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=WeatherDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
