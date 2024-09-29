using BlogApp.Domain.Entities;
using FluentAssertions;

namespace BlogApp.Tests.Domain;

public class PostTests
{
    [Fact]
    public void CreatePost_ShouldInitializeCorrectly_WhenValidArgumentsAreProvided()
    {
        // Arrange
        var title = "Valid Title";
        var content = "Valid content for the post.";
        var authorId = 1;

        // Act
        var post = new Post(title, content, authorId);

        // Assert
        post.Title.Should().Be(title);
        post.Content.Should().Be(content);
        post.AuthorId.Should().Be(authorId);
        post.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1)); 
    }

    [Fact]
    public void SetTitle_ShouldThrowArgumentException_WhenTitleIsEmpty()
    {
        // Arrange
        var post = new Post("Valid Title", "Valid Content", 1);

        // Act
        Action action = () => post.SetTitle(string.Empty);

        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("O título não pode ser vazio.");
    }

    [Fact]
    public void SetContent_ShouldThrowArgumentException_WhenContentIsEmpty()
    {
        // Arrange
        var post = new Post("Valid Title", "Valid Content", 1);

        // Act
        Action action = () => post.SetContent(string.Empty);

        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("O conteúdo não pode ser vazio.");
    }

    [Fact]
    public void SetTitle_ShouldUpdateTitle_WhenValidTitleIsProvided()
    {
        // Arrange
        var post = new Post("Old Title", "Valid Content", 1);
        var newTitle = "New Valid Title";

        // Act
        post.SetTitle(newTitle);

        // Assert
        post.Title.Should().Be(newTitle);
    }

    [Fact]
    public void SetContent_ShouldUpdateContent_WhenValidContentIsProvided()
    {
        // Arrange
        var post = new Post("Valid Title", "Old Content", 1);
        var newContent = "New Valid Content";

        // Act
        post.SetContent(newContent);

        // Assert
        post.Content.Should().Be(newContent);
    }
}
