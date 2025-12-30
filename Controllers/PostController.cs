using BoardApi.Common.Dtos;
using BoardApi.Dtos;
using BoardApi.Models;
using BoardApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoardApi.Controllers;

[ApiController]
[Route("api/posts")]
public class PostController(IPostService postService) : ControllerBase
{
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
        
        try
        {
            await _postService.EditPostBy(id, request.Title, request.Content);
        }
        catch(Exception)
        {
            return NotFound("Post not found");
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePostBy(int id)
    {
        try
        {
            await _postService.DeletePostBy(id);
        }
        catch(Exception)
        {
            return NotFound("Post not found");
        }

        return NoContent();
    }
}