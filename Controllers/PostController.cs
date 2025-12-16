using BoardApi.Dtos;
using BoardApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoardApi.Controllers;

[ApiController]
[Route("api/posts")]
public class PostController : ControllerBase
{
    private static readonly List<Post> posts = [];
    private static int nextId = 1;

    [HttpGet]
    public ActionResult<IEnumerable<Post>> GetPosts()
    {
        return Ok(posts.OrderByDescending(post => post.CreatedAt));
    }

    [HttpPost]
    public ActionResult<Post> CreatePost([FromBody] CreatePostRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest("Title is required.");
        }

        var post = new Post
        {
            Id = nextId++,
            Title = request.Title,
            Content = request.Content ?? ""
        };

        posts.Add(post);

        return CreatedAtAction(nameof(GetPostBy), post);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Post> GetPostBy(int id)
    {
        var post = posts.FirstOrDefault(post => post.Id == id);

        if (post is null)
        {
            return NotFound("Post not found.");
        }

        return Ok(post);
    }

    [HttpPut("{id:int}")]
    public IActionResult EditPostBy(int id, [FromBody] EditPostRequest request)
    {
        var post = posts.FirstOrDefault(post => post.Id == id);

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
    public IActionResult DeletePostBy(int id)
    {
        var idx = posts.FindIndex(post => post.Id == id);

        if (idx < 0)
        {
            return NotFound("Post not found");
        }

        posts.RemoveAt(idx);

        return NoContent();
    }
}