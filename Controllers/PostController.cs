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
}