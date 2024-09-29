using BlogApp.Domain.Entities;
using FluentAssertions;

namespace BlogApp.Tests.Domain;

public class TokenJwtTests
{
    [Fact]
    public void TokenJwt_ShouldInitializeCorrectly()
    {
        // Arrange
        var userId = "1";
        var userName = "testuser";
        var token = "sample.jwt.token";
        var expiration = DateTime.UtcNow.AddHours(1);

        // Act
        var tokenJwt = new TokenJwt
        {
            IdUser = userId,
            UserName = userName,
            Token = token,
            Expiration = expiration
        };

        // Assert
        tokenJwt.IdUser.Should().Be(userId);
        tokenJwt.UserName.Should().Be(userName);
        tokenJwt.Token.Should().Be(token);
        tokenJwt.Expiration.Should().BeCloseTo(expiration, TimeSpan.FromSeconds(1));
    }
}
