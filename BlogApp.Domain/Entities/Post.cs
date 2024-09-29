namespace BlogApp.Domain.Entities;

public class Post
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public int AuthorId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public User Author { get; }

    public Post(string title, string content, int authorId)
    {
        SetTitle(title);
        SetContent(content);
        AuthorId = authorId;
        CreatedAt = DateTime.UtcNow;
    }
    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("O título não pode ser vazio.");
        }
        Title = title;
    }
    public void SetContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("O conteúdo não pode ser vazio.");
        }
        Content = content;
    }
}

