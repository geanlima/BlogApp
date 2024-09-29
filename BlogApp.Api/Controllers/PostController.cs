using BlogApp.Application.DTOs.Requests;
using BlogApp.Application.DTOs.Responses;
using BlogApp.Application.Helpers;
using BlogApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Api.Controllers;

[Route("api/v1")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
        
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostResponse>>> GetAllPosts()
    {
        var posts = await _postService.GetAllPostsAsync();
        return Ok(posts);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<PostResponse>> CreatePost([FromBody] PostRequest postRequest)
    {
        var _userId = UserHelper.GetUserId(HttpContext);
        var post = await _postService.CreatePostAsync(postRequest, _userId.Value);
        return CreatedAtAction(nameof(GetAllPosts), new { id = post.Id }, post);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> EditPost(int id, [FromBody] PostRequest postRequest)
    {
        var _userId = UserHelper.GetUserId(HttpContext);
        await _postService.EditPostAsync(id, postRequest, _userId.Value);

        var response = await _postService.GetPostsByIdAsync(id);

        return Ok(response);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var userId = UserHelper.GetUserId(HttpContext);
        if (userId == null)
        {
            return Unauthorized();
        }

        try
        {
            await _postService.DeletePostAsync(id, userId.Value);
            return Ok(new { message = "Registro deletado com sucesso" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocorreu um erro ao deletar o registro.", details = ex.Message });
        }
    }
}
