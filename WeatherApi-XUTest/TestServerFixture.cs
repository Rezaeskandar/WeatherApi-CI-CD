using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;

public partial class WeatherForecastControllerTests
{
    public class TestServerFixture
    {
        public TestServer Server { get; }

        public TestServerFixture()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddRouting();
                })
                .Configure(app =>
                {
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapGet("/API-health", () =>
                        {
                            return ("API is healthy " + Microsoft.AspNetCore.Http.StatusCodes.Status200OK);
                        });
                    });
                });

            Server = new TestServer(builder);
        }
    }
}




