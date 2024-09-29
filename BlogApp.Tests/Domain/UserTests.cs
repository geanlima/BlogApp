using BlogApp.Domain.Entities;
using FluentAssertions;

namespace BlogApp.Tests.Domain;

public class UserTests
{
    [Fact]
    public void CreateUser_ShouldInitializeCorrectly_WhenValidArgumentsAreProvided()
    {
        // Arrange
        var username = "ValidUser";
        var email = "user@example.com";
        var passwordHash = "hashedpassword123";

        // Act
        var user = new User(username, email, passwordHash);

        // Assert
        user.Username.Should().Be(username);
        user.Email.Should().Be(email);
        user.PasswordHash.Should().Be(passwordHash);
    }

    [Fact]
    public void SetUsername_ShouldThrowArgumentException_WhenUsernameIsEmpty()
    {
        // Arrange
        var user = new User("ValidUser", "user@example.com", "hashedpassword123");

        // Act
        Action action = () => user.SetUsername(string.Empty);

        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("O nome de usuário não pode ser vazio.");
    }

    [Fact]
    public void SetEmail_ShouldThrowArgumentException_WhenEmailIsInvalid()
    {
        // Arrange
        var user = new User("ValidUser", "user@example.com", "hashedpassword123");

        // Act
        Action action = () => user.SetEmail("invalidemail");

        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("O email deve ser válido.");
    }

    [Fact]
    public void SetPasswordHash_ShouldThrowArgumentException_WhenPasswordIsEmpty()
    {
        // Arrange
        var user = new User("ValidUser", "user@example.com", "hashedpassword123");

        // Act
        Action action = () => user.SetPasswordHash(string.Empty);

        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("A senha não pode ser vazia.");
    }
}
