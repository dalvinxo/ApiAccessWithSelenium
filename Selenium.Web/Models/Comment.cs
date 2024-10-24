namespace Selenium.Web.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int? ParentCommentId { get; set; }  // Para poder hacer replies a comentarios
    public Comment ParentComment { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public ICollection<Comment> Replies { get; set; }
}