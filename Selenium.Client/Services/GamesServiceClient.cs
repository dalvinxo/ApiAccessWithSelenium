using System.Net.Http.Json;
using Selenium.Client.Interfaces;
using Selenium.Client.Models;

namespace Selenium.Client.Services;

public class GamesServiceClient : IGamesServiceClient
{

    private readonly HttpClient _httpClient;

    public GamesServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<GameResponse>> GetDataAsync(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<List<GameResponse>>();

        return result ?? [];
    }

    public async Task<GameDetailsResponse> GetDataByIdAsync(int Id)
    {
        var response = await _httpClient.GetAsync("/api/game?id=" + Id);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<GameDetailsResponse>();

        return result;


    }

}