using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using WeatherApi_CI_CD.Data;
using WeatherApi_CI_CD.Model;
using WeatherApi_XUTest;
using Xunit;

//
public partial class WeatherForecastControllerTests
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public WeatherForecastControllerTests()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
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
        // Add these lines to check each property
        Assert.Equal("Temperature", forecast.WeatherData.Temperature.Name);
        Assert.Equal(33, forecast.WeatherData.Temperature.Value);
        Assert.Equal("C", forecast.WeatherData.Temperature.Unit);
        Assert.Equal("Humidity", forecast.WeatherData.Humidity.Name);
        Assert.Equal(65, forecast.WeatherData.Humidity.Value);
        Assert.Equal("%", forecast.WeatherData.Humidity.Unit);
        Assert.Equal("Wind", forecast.WeatherData.Wind.Name);
        Assert.Equal(12.5, forecast.WeatherData.Wind.Value);
        Assert.Equal("km/h", forecast.WeatherData.Wind.Unit);
    }



    // health check 

    public class HealthCheckTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public HealthCheckTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
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

    // added counter test 
    public class APICounterTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public APICounterTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

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
            int initialCount = 0;
            var counter = new APICounter(initialCount);

            // Act
            var statistics = await counter.GetStatisticsAsync();

            // Assert
            Assert.Equal("Number of API calls: 0", statistics);

        }

        [Fact]
        public async Task GetStatistice_Return()
        {
            // Act
            var response = await _client.GetAsync("/API/call/statistics");

            // Assert
            response.EnsureSuccessStatusCode();
            var message = await response.Content.ReadAsStringAsync();
            Assert.Equal($"Number of API calls: 1", message);
        }
    }
    // Save City

    public class FavoriteCityEndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public FavoriteCityEndpointTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task AddCityToDatabase_ReturnsCreatedResponse()
        {
            // Arrange
            var client = _factory.CreateClient();
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new DataContext(options))
            {
                var city = new City { Name = "stockholm" };
                var json = new StringContent(JsonSerializer.Serialize(city), Encoding.UTF8, "application/json");
                // By creating a StringContent instance with the serialized JSON, you can include it as the content of an HTTP request, such as a POST request, to send the JSON data to a web API endpoint.

                // Act
                var response = await client.PostAsync("/favorite-city", json);

                // Assert
                response.EnsureSuccessStatusCode();
                //Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }
    }
}



