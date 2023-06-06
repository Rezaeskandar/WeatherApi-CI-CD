using Microsoft.EntityFrameworkCore;

namespace WeatherApi_CI_CD
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<City> Cities { get; set; }
    }
}
