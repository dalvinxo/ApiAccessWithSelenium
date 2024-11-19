using Selenium.Client.Interfaces;
using Selenium.Client.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var baseUrlServiceGame = builder.Configuration["GameServices:BaseUrl"];

builder.Services.AddHttpClient<IGamesServiceClient, GamesServiceClient>(client =>
{
    client.BaseAddress = new Uri(baseUrlServiceGame);
});

//https://www.freetogame.com/api-doc?ref=freepublicapis.com
//https://www.freetogame.com/api/games?platform=pc



builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder => builder.WithOrigins("https://localhost:44472", "https://localhost:44471", "http://localhost:4200") // Cambia esto por la URL de tu frontend
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
        });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.MapGet("api/games", async (IGamesServiceClient gamesServiceClient) =>
{
    var games = await gamesServiceClient.GetDataAsync("/api/games?platform=pc");
    return games is not null ? Results.Ok(games) : Results.Problem("Error retrieving data.");
})
.WithName("GetGames")
.WithOpenApi();

app.MapGet("api/games/{id}", async (int id, IGamesServiceClient gamesServiceClient) =>
{
    var games = await gamesServiceClient.GetDataByIdAsync(id);
    return games is not null ? Results.Ok(games) : Results.Problem("Error retrieving data.");
})
.WithName("GetGamesById")
.WithOpenApi();


app.Run();
