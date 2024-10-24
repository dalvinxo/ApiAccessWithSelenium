namespace Selenium.Web.Models;

public class Post
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int Rate { get; set; }
    public int GameId { get; set; }
    public Game Game { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public ICollection<Comment> Comments { get; set; }
}
