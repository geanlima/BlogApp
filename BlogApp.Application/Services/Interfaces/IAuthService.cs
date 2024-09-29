using BlogApp.Application.DTOs.Requests;
using BlogApp.Domain.Entities;

namespace BlogApp.Application.Services.Interfaces;

public interface IAuthService
{
    Task<TokenJwt> AuthenticateAsync(LoginRequest loginRequest);
}
