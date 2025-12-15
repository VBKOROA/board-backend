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

        return StatusCode(201, post);
    }
}