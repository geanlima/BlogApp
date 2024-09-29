using BlogApp.Application.DTOs.Requests;
using BlogApp.Application.DTOs.Responses;

namespace BlogApp.Application.Services.Interfaces;

public interface IPostService
{
    Task<IEnumerable<PostResponse>> GetAllPostsAsync();
    Task<PostResponse> CreatePostAsync(PostRequest postRequest, int userId);
    Task<PostResponse> GetPostsByIdAsync(int id);
    Task<IEnumerable<PostResponse>> GetPostsByUserIdAsync(int userId);
    Task EditPostAsync(int postId, PostRequest postRequest, int userId);
    Task DeletePostAsync(int postId, int userId);
}
