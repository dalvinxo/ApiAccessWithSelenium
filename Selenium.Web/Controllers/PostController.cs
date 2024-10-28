using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Selenium.Web.Context;
using Selenium.Web.Models;

namespace Selenium.Web.Controllers;

public class PostController : Controller
{
    private readonly ILogger<PostController> _logger;
    private readonly GameService _gameService;
    private readonly GameDbContext _gameDbContext;


    public PostController(ILogger<PostController> logger, GameService gameService, GameDbContext gameDbContext)
    {
        _logger = logger;
        _gameService = gameService;
        _gameDbContext = gameDbContext;
    }

    public async Task<IActionResult> Index()
    {

        var posts = await _gameDbContext.Posts
        .Include(x => x.Game)
        .Include(x => x.Person)
        .Include(x => x.Comments)
        .ThenInclude(c => c.Replies)
        .ToListAsync();

        return View(posts);

    }

    public async Task<IActionResult> Create(int Id)
    {

        var model = new GameFormModel
        {
            GameId = Id,
            PaisId = 47,
            Genre = "Hombre"
        };

        ViewBag.Paises = _gameDbContext.Paises.Select(p => new SelectListItem
        {
            Value = p.Id.ToString(),
            Text = p.Name
        }).ToList();

        return View(model);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GameFormModel model)
    {
        if (ModelState.IsValid)
        {

            var game = await _gameService.GetGamesByIdAsync(model.GameId);

            if (game == null)
            {
                return NotFound();
            }

            var gameDb = await _gameDbContext.Games.FirstOrDefaultAsync(x => x.Title.Contains(game.Title));


            if (gameDb == null)
            {

                var newGame = new Game()
                {
                    Title = game.Title,
                    ShortDescription = game.ShortDescription,
                    Thumbnail = game.Thumbnail
                };

                _gameDbContext.Games.Add(newGame);
                await _gameDbContext.SaveChangesAsync();

                gameDb = newGame;
            }


            var personDb = await _gameDbContext.People.FirstOrDefaultAsync(x => x.Name == model.Name);


            if (personDb == null)
            {

                var newPerson = new Person()
                {
                    Age = model.Age,
                    Name = model.Name,
                    Genre = model.Genre,
                    PaisId = model.PaisId
                };

                _gameDbContext.People.Add(newPerson);
                await _gameDbContext.SaveChangesAsync();

                personDb = newPerson;


            }
            else
            {

                personDb.Age = model.Age;
                personDb.Genre = model.Genre;
                personDb.PaisId = model.PaisId;

                await _gameDbContext.SaveChangesAsync();

            }



            var newPost = new Post()
            {
                Title = model.Title,
                Description = model.Description,
                Rate = model.Rate,
                GameId = gameDb.Id,
                PersonId = personDb.Id,
                CreatedDate = DateTime.Now,
            };


            _gameDbContext.Posts.Add(newPost);
            await _gameDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        ViewBag.Paises = _gameDbContext.Paises.Select(p => new SelectListItem
        {
            Value = p.Id.ToString(),
            Text = p.Name
        }).ToList();

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> AddComment(int postId, string content, int? parentCommentId)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            // Puedes añadir un mensaje de error o redirigir con una advertencia
            return RedirectToAction("Index");
        }

        var comment = new Comment
        {
            Content = content,
            PostId = postId,
            ParentCommentId = parentCommentId,
            CreatedDate = DateTime.Now,
            Estado = true
        };

        _gameDbContext.Comments.Add(comment);
        await _gameDbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }


}
