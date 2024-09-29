namespace BlogApp.Application.DTOs.Responses;

public class PostResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string AuthorName { get; set; }
}
