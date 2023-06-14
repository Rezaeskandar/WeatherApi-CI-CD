using System;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Json;
using WeatherApi_XUTest;
using WeatherApi_CI_CD;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;

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
        Assert.Equal(23, forecast.WeatherData.Temperature.Value);
        Assert.Equal("°C", forecast.WeatherData.Temperature.Unit);
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



