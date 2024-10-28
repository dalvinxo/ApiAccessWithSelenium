using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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


    public async Task<IActionResult> Create(int Id)
    {

        var model = new GameFormModel
        {
            GameId = Id,
            PaisId = 1 // Valor por defecto (puedes cambiarlo si deseas)
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
    public IActionResult Create(GameFormModel model)
    {
        if (ModelState.IsValid)
        {
            // Guardar en la base de datos o hacer lo necesario
            // Ejemplo: _context.Games.Add(new Game { /* Map properties */ });
            // _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Paises = _gameDbContext.Paises.Select(p => new SelectListItem
        {
            Value = p.Id.ToString(),
            Text = p.Name
        }).ToList();

        return View(model);
    }


}
