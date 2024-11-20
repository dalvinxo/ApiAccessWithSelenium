namespace Selenium.API.Dtos;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Rate { get; set; }
    public string GameTitle { get; set; }
    public string Thumbnail { get; set; }
    public string PersonName { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public List<CommentDto> Comments { get; set; }
}
