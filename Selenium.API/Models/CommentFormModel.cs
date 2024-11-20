using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
public class CommentFormModel
{
    public int PostId { get; set; }
    public string Content { get; set; }
    public int? ParentCommentId { get; set; }
}