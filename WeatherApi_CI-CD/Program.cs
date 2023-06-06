


//[x]As a user of the API, I want to be able to get current weather data (temperature, humidity, wind) for Stockholm.

using Microsoft.EntityFrameworkCore;

//[]Som användare av API:et vill jag kunna spara en favoritstad och slippa ange den varje gång(Obs att det bara ska sparas så länge appen körs, alltså inte mellan körningar)
//[]Som systemägare vill jag kunna se om API:et körs(health check)
//[]Som systemägare vill jag kunna se statistik på antal anrop sen API:et startades
//[]Som slutanvändare av Reactklienten vill jag kunna se aktuellt väder för Stockholm
//[]Som slutanvändare av Reactklienten vill jag kunna se och spara favoritstad

using WeatherApi_CI_CD;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(c => c.UseInMemoryDatabase("City"));//here we have used memory database

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast-Stockholm", () =>
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
})
.WithName("GetCurrentWeatherData");


app.MapGet("/API-health", () =>
{
    return ($"API is healthy {StatusCodes.Status200OK}");
});



app.MapPost("/favorite-city", async (City city, DataContext db) =>
{
    db.Cities.Add(city);
    await db.SaveChangesAsync();
    return Results.Created($"/save/{city.Name}", city);

});


app.Run();

public record WeatherForecast(DateTime Date, WeatherData WeatherData);

public record WeatherData(string Location, WeatherProperty Temperature, WeatherProperty Humidity, WeatherProperty Wind);

public record WeatherProperty(string Name, double Value, string Unit);
