using BlogApp.Domain.Entities;
using BlogApp.Domain.Interfaces;
using BlogApp.Infrastructure.Data;
using BlogApp.Infrastructure.WebSockets;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly BlogDbContext _context;
    private readonly IUserRepository _userRepository;
    public PostRepository(BlogDbContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await _context.Posts.Include(p => p.Author).ToListAsync();
    }

    public async Task<Post> GetByIdAsync(int postId)
    {
        return await _context.Posts.Include(p => p.Author).FirstOrDefaultAsync(p => p.Id == postId);
    }

    public async Task AddAsync(Post post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();

        var user = await _userRepository.GetByIdAsync(post.AuthorId);

        await WebSocketHandler.NotifyAllClientsAsync($"Novo post: {post.Title} foi adicionado por {user.Username}.");
    }

    public async Task UpdateAsync(Post post)
    {
        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Post post)
    {
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }
}
