namespace Selenium.API.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int? ParentCommentId { get; set; }
    public Comment ParentComment { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public bool Estado { get; set; } = true;
    public ICollection<Comment> Replies { get; set; }
}