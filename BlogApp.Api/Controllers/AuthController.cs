using BlogApp.Application.DTOs.Requests;
using BlogApp.Application.DTOs.Responses;
using BlogApp.Application.Services;
using BlogApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var token = await _authService.AuthenticateAsync(loginRequest);
        
        if (token == null)
            return Unauthorized();

        return Ok(token);
    }
}
