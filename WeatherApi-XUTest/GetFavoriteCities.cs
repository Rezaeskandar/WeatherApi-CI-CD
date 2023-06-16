﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using WeatherApi_CI_CD.Data;
using WeatherApi_CI_CD.Model;


public class GetFavoriteCities
{
    [Fact]
    public async Task GetFavoriteCities_ReturnsListOfCities()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var context = new DataContext(options))
        {
            // Prepare the test data
            var cities = new List<City>
        {
            new City { Name = "stockholm" },
            new City { Name = "gothenburg" },
            new City { Name = "malmö" }
        };
            context.Cities.AddRange(cities);
            context.SaveChanges();

            var controller = new GetFavoriteCitiesController(context);

            // Act
            var result = await controller.GetFavoriteCities();

            // Assert 
            var returnedCities = Assert.IsAssignableFrom<List<City>>(result.Value);
            Assert.NotEmpty(returnedCities);
        }
    }
}

internal class GetFavoriteCitiesController
{
    private DataContext context;

    public GetFavoriteCitiesController(DataContext context)
    {
        this.context = context;
    }
    public async Task<ActionResult<List<City>>> GetFavoriteCities()
    {
        var cities = await context.Cities.ToListAsync();
        return cities;
    }
}