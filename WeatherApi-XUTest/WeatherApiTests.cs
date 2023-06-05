using System;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Net.Http.Json;
using WeatherApi_XUTest;

//
public partial class WeatherForecastControllerTests
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
        var response = await _client.GetAsync("/weatherforecast-Stockholm");

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


    // health check 

    public class HealthCheckTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public HealthCheckTests(TestServerFixture fixture)
        {
            _server = fixture.Server;
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task GetHealth_ReturnsHealthyMessage()
        {
            // Act
            var response = await _client.GetAsync("/API-health");

            // Assert
            response.EnsureSuccessStatusCode();
            var message = await response.Content.ReadAsStringAsync();
            Assert.Equal("API is healthy 200", message);
        }
    }
}



