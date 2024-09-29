using BlogApp.Application.DTOs.Requests;
using BlogApp.Application.Services.Interfaces;
using BlogApp.Domain.Entities;
using BlogApp.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogApp.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;

        // Acesse a chave secreta do appsettings.json
        _secretKey = configuration.GetValue<string>("Jwt:SecretKey");
        _issuer = configuration.GetValue<string>("Jwt:Issuer");
        _audience = configuration.GetValue<string>("Jwt:Audience");
    }

    public async Task<TokenJwt> AuthenticateAsync(LoginRequest loginRequest)
    {
        var user = await _userRepository.GetByUsernameAsync(loginRequest.Username);
        if (user == null || user.PasswordHash != HashPassword(loginRequest.Password))
        {
            return null;
        }

        var token = GenerateJwtToken(user);

        token.IdUser = user.Id.ToString();
        token.UserName = user.Username;
        token.Token = token.Token;
        token.Expiration = token.Expiration;

        return token;
    }

    private string HashPassword(string password)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
    }

    private TokenJwt GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim("userId", user.Id.ToString()),
            new Claim("userName", user.Username.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddHours(24);

        JwtSecurityToken token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

        var resultToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new TokenJwt()
        {
            Token = resultToken,
            Expiration = expiration
        };

    }
}
