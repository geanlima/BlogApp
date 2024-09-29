using AutoMapper;
using BlogApp.Application.DTOs.Requests;
using BlogApp.Application.DTOs.Responses;
using BlogApp.Application.Services.Interfaces;
using BlogApp.Domain.Entities;
using BlogApp.Domain.Interfaces;

namespace BlogApp.Application.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public PostService(IPostRepository postRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PostResponse>> GetAllPostsAsync()
    {
        var posts = await _postRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PostResponse>>(posts);
    }

    public async Task<PostResponse> CreatePostAsync(PostRequest postRequest, int userId)
    {
        var post = new Post(postRequest.Title, postRequest.Content, userId);
        await _postRepository.AddAsync(post);

        var postWithAuthor = await _postRepository.GetByIdAsync(post.Id);

        return _mapper.Map<PostResponse>(postWithAuthor);
    }

    public async Task EditPostAsync(int postId, PostRequest postRequest, int userId)
    {
        var post = await _postRepository.GetByIdAsync(postId);
        if (post == null || post.AuthorId != userId)
        {
            throw new UnauthorizedAccessException("Você não pode editar esta postagem.");
        }

        post.SetTitle(postRequest.Title);
        post.SetContent(postRequest.Content);

        await _postRepository.UpdateAsync(post);
    }

    public async Task DeletePostAsync(int postId, int userId)
    {
        var post = await _postRepository.GetByIdAsync(postId);
        if (post == null || post.AuthorId != userId)
        {
            throw new UnauthorizedAccessException("Você não pode deletar esta postagem.");
        }

        await _postRepository.DeleteAsync(post);
    }

    public async Task<PostResponse> GetPostsByIdAsync(int id)
    {
        var post = await _postRepository.GetByIdAsync(id);

        return _mapper.Map<PostResponse>(post);
    }

    public Task<IEnumerable<PostResponse>> GetPostsByUserIdAsync(int userId)
    {
        throw new NotImplementedException();
    }
}
