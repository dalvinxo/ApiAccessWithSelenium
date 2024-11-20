namespace Selenium.API.Models;

public class Game
{

    public int Id { get; set; }
    public string Title { get; set; }
    public string Thumbnail { get; set; }
    public string ShortDescription { get; set; }
    public ICollection<Post> Posts { get; set; }

}
