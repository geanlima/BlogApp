using BlogApp.Application.DTOs.Requests;
using BlogApp.Application.DTOs.Responses;

namespace BlogApp.Application.Services.Interfaces;

public interface IUserService
{
    Task<UserResponse> CreateUserAsync(UserRequest request);
    Task<UserResponse> GetUserByIdAsync(int id);
    Task<IEnumerable<UserResponse>> GetAllUsersAsync();
    Task UpdateUserAsync(int id, UserRequest request);
    Task DeleteUserAsync(int id);
}
