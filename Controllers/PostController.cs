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

        return Ok(new GetPostByResponse(post));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditPostBy(int id, [FromBody] EditPostRequest request)
    {

        await _postService.EditPostBy(id, request.Title, request.Content);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePostBy(int id)
    {
        await _postService.DeletePostBy(id);

        return NoContent();
    }

    [HttpPost("{postId:int}/comments")]
    public async Task<ActionResult<WriteCommentResponse>> WriteComment(int postId, [FromBody] WriteCommentRequest request)
    {
        var result = await _postService.WriteCommentTo(postId, request.Contents);
        return StatusCode(201, new WriteCommentResponse(result));
    }

    [HttpGet("{postId:int}/comments")]
    public async Task<ActionResult<IReadOnlyList<CommentDto>>> GetCommentsBy(int postId)
    {
        return Ok(await _postService.GetCommentsBy(postId));
    }
}