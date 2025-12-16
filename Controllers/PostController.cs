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
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    {
        var posts = await db.Posts.OrderByDescending(post => post.CreatedAt).ToListAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost([FromBody] CreatePostRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest("Title is required.");
        }

        var post = new Post
        {
            Title = request.Title,
            Content = request.Content ?? ""
        };

        db.Posts.Add(post);
        await db.SaveChangesAsync();

        return StatusCode(201, post);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Post>> GetPostBy(int id)
    {
        var post = await db.Posts.FindAsync(id);

        if (post is null)
        {
            return NotFound("Post not found.");
        }

        return Ok(post);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditPostBy(int id, [FromBody] EditPostRequest request)
    {
        var post = await db.Posts.FindAsync(id);

        if (post is null)
        {
            return NotFound("Post not found");
        }

        if (request.Title is null && request.Content is null)
        {
            return BadRequest("One of property shouldn't be null.");
        }

        if (request.Title is not null)
        {
            post.Title = request.Title;
        }

        if (request.Content is not null)
        {
            post.Content = request.Content;
        }

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