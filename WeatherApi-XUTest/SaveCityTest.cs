using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class FavoriteCityEndpointTests
{
    [Fact]
    public async Task AddCityToDatabase_ReturnsCreatedResponse()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var context = new DataContext(options))
        {
            var controller = new FavoriteCityController(context);

            var city = new City { Name = "Stockholm" };

            // Act
            var result = await controller.AddCity(city);

            // Assert

            Assert.IsType<NotFoundResult>(result);
            // Assert.IsType<CreatedAtActionResult>(result);
        }
    }
}

public class FavoriteCityController
{
    private readonly DataContext _context;

    public FavoriteCityController(DataContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> AddCity(City city)
    {
        _context.Cities.Add(city);
        await _context.SaveChangesAsync();
        return new CreatedAtActionResult("GetCity", "Cities", new { id = city.Id }, city);
    }
}

