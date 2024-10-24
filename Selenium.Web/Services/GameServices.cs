using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Selenium.Web.Models;

public class GameService
{
    private readonly HttpClient _httpClient;

    public GameService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Games>> GetGamesAsync()
    {
        var response = await _httpClient.GetAsync("http://localhost:5026/games");

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Games>>(jsonResponse) ?? new List<Games>();
        }

        return new List<Games>();
    }
}
