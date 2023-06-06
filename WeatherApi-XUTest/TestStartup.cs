using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;

namespace WeatherApi_XUTest
{
    public class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/weatherforecast-Stockholm", () =>
                {
                    var forecast = new WeatherForecast
                    (
                        DateTime.Now,
                        new WeatherData
                        (
                            "Stockholm",
                            new WeatherProperty("Temperature", 23, "°C"),
                            new WeatherProperty("Humidity", 65, "%"),
                            new WeatherProperty("Wind", 12.5, "km/h")
                        )
                    );

                    return forecast;
                });
            });
        }
    }
}