using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Selenium.API.Context;
using Selenium.API.Dtos;
using Selenium.API.Models;
using Selenium.Client.Interfaces;
using Selenium.Client.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });


builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseSqlServer(connectionString));

var baseUrlServiceGame = builder.Configuration["GameServices:BaseUrl"];

builder.Services.AddHttpClient<IGamesServiceClient, GamesServiceClient>(client =>
{
    client.BaseAddress = new Uri(baseUrlServiceGame);
});

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

app.MapPost("api/posts", async (GameDbContext db, GameFormModel model, IGamesServiceClient gameService) =>
{
    if (string.IsNullOrEmpty(model.Title) || model.Rate < 1 || model.Rate > 10)
        return Results.BadRequest("Invalid data");

    var game = await gameService.GetDataByIdAsync(model.GameId);
    if (game == null)
        return Results.NotFound("Game not found");

    var gameDb = await db.Games.FirstOrDefaultAsync(x => x.Title.Contains(game.title)) ??
                 new Game
                 {
                     Title = game.title,
                     ShortDescription = game.short_description,
                     Thumbnail = game.thumbnail
                 };

    db.Games.Add(gameDb);
    await db.SaveChangesAsync();

    var personDb = await db.People.FirstOrDefaultAsync(x => x.Name == model.Name) ??
                   new Person
                   {
                       Name = model.Name,
                       Age = model.Age,
                       Genre = model.Genre,
                       PaisId = model.PaisId
                   };

    db.People.Add(personDb);
    await db.SaveChangesAsync();

    var newPost = new Post
    {
        Title = model.Title,
        Description = model.Description,
        Rate = model.Rate,
        GameId = gameDb.Id,
        PersonId = personDb.Id,
        CreatedDate = DateTime.Now,
    };

    db.Posts.Add(newPost);
    await db.SaveChangesAsync();

    return Results.Created($"/posts/{newPost.Id}", newPost);
})
.WithName("PostCreatePosts")
.WithOpenApi();

app.MapGet("api/posts", async (GameDbContext db) =>
{
    var posts = await db.Posts
        .AsNoTracking()
        .Include(x => x.Game)
        .Include(x => x.Person)
        .Include(x => x.Comments)
        .ThenInclude(c => c.Replies)
        .Select(x => new PostDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Rate = x.Rate,
            PersonName = x.Person.Name,
            GameTitle = x.Game.Title,
            Thumbnail = x.Game.Thumbnail,
            CreatedDate = x.CreatedDate,
            Comments = x.Comments.Select(c => new CommentDto()
            {
                Content = c.Content,
                CreatedDate = x.CreatedDate,
                Id = c.Id
            }).ToList()
        })
        .ToListAsync();

    return Results.Ok(posts);
})
.WithName("GetPostAll")
.WithOpenApi();

app.MapPost("api/comments", async (GameDbContext db, CommentFormModel model, IGamesServiceClient gameService) =>
{
    if (string.IsNullOrWhiteSpace(model.Content))
    {
        return Results.BadRequest("El contenido no puede estar vacío.");
    }

    var comment = new Comment
    {
        Content = model.Content,
        PostId = model.PostId,
        ParentCommentId = model.ParentCommentId,
        CreatedDate = DateTime.Now,
        Estado = true
    };

    db.Comments.Add(comment);
    await db.SaveChangesAsync();

    return Results.Ok(new CommentDto()
    {
        Content = comment.Content,
        CreatedDate = comment.CreatedDate,
        Id = comment.Id,
        parentCommentId = comment.ParentCommentId
    });
})
.WithName("PostComments")
.WithOpenApi();




app.Run();
