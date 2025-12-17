using BoardApi.Common.Dtos;
using BoardApi.Data;
using BoardApi.Dtos;
using BoardApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardApi.Controllers;

[ApiController]
[Route("api/posts")]
public class PostController(AppDbContext db) : ControllerBase
{
    private readonly AppDbContext db = db;

    [HttpGet]
    public async Task<ActionResult<CommonPageResponse<Post>>> GetPosts([FromQuery] GetPostsByQueryParam param)
    {
        var posts = await db.Posts
            .OrderByDescending(post => post.CreatedAt)
            .Skip(param.Page * param.PageSize)
            .Take(param.PageSize)
            .ToListAsync();
            
        return Ok(new CommonPageResponse<Post>(posts, param.Page, param.PageSize));
    }

    [HttpPost]
    public async Task<ActionResult<CreatePostResponse>> CreatePost([FromBody] CreatePostRequest request)
    {

        var post = new Post
        {
            Title = request.Title,
            Content = request.Content ?? ""
        };

        db.Posts.Add(post);
        await db.SaveChangesAsync();

        return StatusCode(201, new CreatePostResponse(post));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetPostByResponse>> GetPostBy(int id)
    {
        var post = await db.Posts.FindAsync(id);

        if (post is null)
        {
            return NotFound("Post not found.");
        }

        return Ok(new GetPostByResponse(post));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditPostBy(int id, [FromBody] EditPostRequest request)
    {
        var post = await db.Posts.FindAsync(id);

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

        await db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePostBy(int id)
    {
        var post = await db.Posts.FindAsync(id);

        if (post is null)
        {
            return NotFound("Post not found");
        }

        db.Posts.Remove(post);
        await db.SaveChangesAsync();

        return NoContent();
    }
}