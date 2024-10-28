using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Selenium.Web.Models;

namespace Selenium.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly GameService _gameService;

    public HomeController(ILogger<HomeController> logger, GameService gameService)
    {
        _logger = logger;
        _gameService = gameService;
    }

    public async Task<IActionResult> Index(int? pageNumber)
    {

        var games = await _gameService.GetGamesAsync();
        int pageSize = 6;
        return View(PaginatedList<Games>.Create(games, pageNumber ?? 1, pageSize));
    }





    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
