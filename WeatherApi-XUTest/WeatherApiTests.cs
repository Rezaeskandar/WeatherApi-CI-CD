using System;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using System.Net.Http.Json;

public class WeatherForecastControllerTests
{
    private readonly TestServer _server;
    private readonly HttpClient _client;

    public WeatherForecastControllerTests()
    {
        var builder = new WebHostBuilder()
            .UseStartup<TestStartup>();

        _server = new TestServer(builder);
        _client = _server.CreateClient();
    }

    [Fact]
    public async Task GetCurrentWeatherData_ReturnsWeatherForecast()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/weatherforecast");

        // Assert
        response.EnsureSuccessStatusCode();
        var forecast = await response.Content.ReadFromJsonAsync<WeatherForecast>();
        Assert.NotNull(forecast);
        Assert.Equal("Stockholm", forecast.WeatherData.Location);
        // Add more assertions for other properties if needed
    }

    public void Dispose()
    {
        _client.Dispose();
        _server.Dispose();
    }
}

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
            endpoints.MapGet("/weatherforecast", () =>
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
