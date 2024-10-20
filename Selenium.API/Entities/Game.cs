namespace Selenium.API.Entities;

public class Game
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Thumbnail { get; set; }
    public string ShortDescription { get; set; }
    public string GameUrl { get; set; }
    public string Genre { get; set; }
    public string Platform { get; set; }
    public string Publisher { get; set; }
    public string Developer { get; set; }
    public string ReleaseDate { get; set; }
    public string FreetogameProfileUrl { get; set; }
    
    // Relación con Post
    public ICollection<Post> Posts { get; set; }
}
