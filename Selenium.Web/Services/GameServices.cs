using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Selenium.Web.Models;
using Selenium.Web.Dto;

public class GameService
{
    private readonly HttpClient _httpClient;

    public GameService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<DtoGame>> GetGamesAsync()
    {
        var response = await _httpClient.GetAsync("http://localhost:5026/games");

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<DtoGame>>(jsonResponse) ?? new List<DtoGame>();
        }

        return new List<DtoGame>();
    }
}
