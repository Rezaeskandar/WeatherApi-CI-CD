using System;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
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

    public class APICounterTests
    {
        [Fact]
        public async Task IncrementAPICall_IncrementsCount()
        {
            // Arrange
            int initialCount = 0;
            var counter = new APICounter(initialCount);

            // Act
            await counter.IncrementAPICallAsync();

            // Assert
            Assert.Equal(1, counter.GetAPICallCount());
        }

        [Fact]
        public async Task GetStatistics_ReturnsFormattedCountString()
        {
            // Arrange
            int initialCount = 5;
            var counter = new APICounter(initialCount);

            // Act
            var statistics = await counter.GetStatisticsAsync();

            // Assert
            Assert.Equal("Number of API calls: 5", statistics);
            
        }
    }
}



