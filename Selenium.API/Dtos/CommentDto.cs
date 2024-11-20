using System;

namespace Selenium.API.Dtos;

public class CommentDto
{
    public int Id { get; set; }
    public int? parentCommentId { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

}
