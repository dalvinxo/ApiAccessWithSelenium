using Microsoft.EntityFrameworkCore;
using Selenium.API.Context;
using Selenium.Client.Interfaces;
using Selenium.Client.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Agregar el DbContext de Entity Framework Core con SQL Server
builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddHttpClient<IGamesServiceClient, GamesServiceClient>(client =>
{
    client.BaseAddress = new Uri("https://www.freetogame.com");
});

//https://www.freetogame.com/api-doc?ref=freepublicapis.com
//https://www.freetogame.com/api/games?platform=pc

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();


app.MapGet("/games", async (IGamesServiceClient gamesServiceClient) =>
{
    var games = await gamesServiceClient.GetDataAsync("/api/games?platform=pc");
    return games is not null ? Results.Ok(games) : Results.Problem("Error retrieving data.");
})
.WithName("GetGames")
.WithOpenApi();


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
