using BoardApi.Common.Dtos;
using BoardApi.Data;
using BoardApi.Dtos;
using BoardApi.Enums;
using BoardApi.Models;
using BoardApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardApi.Controllers;

[ApiController]
[Route("api/posts")]
public class PostController(AppDbContext db, IPostService postService) : ControllerBase
{
    private readonly AppDbContext _db = db;
    private readonly IPostService _postService = postService;

    [HttpGet]
    public async Task<ActionResult<CommonPageResponse<Post>>> GetPosts([FromQuery] GetPostsByQueryParam param)
    {
        var result = await _postService.GetPagedPost(param.Page, param.PageSize, param.Sort, param.Order, param.Keyword);

        return Ok(new CommonPageResponse<Post>(result.Posts, param.Page, param.PageSize, result.TotalPosts));
    }

    [HttpPost]
    public async Task<ActionResult<CreatePostResponse>> CreatePost([FromBody] CreatePostRequest request)
    {

        var post = await _postService.CreatePost(request.Title, request.Content);

        return StatusCode(201, new CreatePostResponse(post));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetPostByResponse>> GetPostBy(int id)
    {
        var post = await _postService.GetPostBy(id);

        if (post is null)
        {
            return NotFound("Post not found.");
        }

        return Ok(new GetPostByResponse(post));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditPostBy(int id, [FromBody] EditPostRequest request)
    {
        var post = await _db.Posts.FindAsync(id);

        if (post is null)
        {
            return NotFound("Post not found");
        }

        if (request.Title is not null)
        {
            post.Title = request.Title;
        }

        if (request.Content is not null)
        {
            post.Content = request.Content;
        }

        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePostBy(int id)
    {
        var post = await _db.Posts.FindAsync(id);

        if (post is null)
        {
            return NotFound("Post not found");
        }

        _db.Posts.Remove(post);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}