using Selenium.Client.Models;

namespace Selenium.Client.Interfaces;

public interface IGamesServiceClient {    
    Task<List<GameResponse>> GetDataAsync(string endpoint);

}