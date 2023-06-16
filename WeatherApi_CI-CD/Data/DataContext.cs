using Microsoft.EntityFrameworkCore;
using WeatherApi_CI_CD.Model;

namespace WeatherApi_CI_CD.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<City> Cities { get; set; }
    }
}


