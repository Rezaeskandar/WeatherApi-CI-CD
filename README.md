# WeatherApi
We are going to do a team project where we build a Weather API using Minimal API in C# and a client with React using Vite.
We will use Test Driven Development process to create this application and will be deploying a CI/CD pipeline.

## Structure of Project:
|   Tasks     |   Framwork    |  Effect  |
|-----|--------|-------|
|C# |  Minimal API   | Out via Swagger
|JS |   React, React-Dom, Vite   | Fetching Data via Endpoints
|Database |   InMemory   | InMemory has limited data
|Model | VS C# & .NET Core 6   | Get and Post
|Connection |  JSON   |  Global Datbase
|TDD |  XUnit Test   | Red-Green-Refactor

## TDD Unit Tests 
The unit tests are written in C# using Xunit framework and they validate the functionality of the WeatherForecastController endpoints. The tests cover different scenarios and ensure that the API behaves as expected.
## Test Cases
#### 1. `GetCurrentWeatherData_ReturnsWeatherForecast`:
- Tests the "GetCurrentWeatherData" endpoint.
- Validates that the response contains a valid WeatherForecast object with specific property values.
#### 2. `HealthCheckTests.GetHealth_ReturnsHealthyMessage:` 
 - Tests the "GetHealth" endpoint.
 - Verifies that the API responds with a healthy message.
#### 3. `APICounterTests.IncrementAPICall_IncrementsCount:`
- Tests the incrementing functionality of the APICounter class.
- Ensures that the APICall count is incremented correctly.
#### 4. `APICounterTests.GetStatistics_ReturnsFormattedCountString:`
- Tests the retrieval of statistics from the APICounter class.
- Checks that the returned statistics string has the expected format.
#### 5. `APICounterTests.GetStatistice_Return:`
- Tests the "GetStatistice" endpoint of.
- Validates that the response contains the correct API call count.
#### 6. `FavoriteCityEndpointTests.AddCityToDatabase_ReturnsCreatedResponse:`
- Tests the "AddCityToDatabase" endpoint.
- Verifies that the API responds with a "Created" status code when adding a city to the database.
#### 7.`GetFavoriteCitiesTests.GetFavoriteCities_ReturnsListOfCities:`
- Tests the "GetFavoriteCities" endpoint.
- Checks that the API responds with a list of cities from the database.
 
   

## Scrum Board Link - Below
https://trello.com/b/iGF6RiYu/ci-cd-scrum-board

## Team

- [Reza](https://github.com/Rezaeskandar)
- [Md. Kamrul Hasan](https://github.com/chasmkhasan)
- [Md Ruhul Amin](https://github.com/Md-Ruhul-Amin-Rony)
- [Tasmia Zahin](https://github.com/tasmiazahin)
- [Suhagan Mostahid](https://github.com/suhagan)
- [Abdirahman Farah](https://github.com/Abfar90)
