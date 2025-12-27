using BoardApi.Models;

namespace BoardApi.Dtos;

public record CreatePostResponse(int Id, string Title, string Content, DateTime CreatedAt)
{
    public CreatePostResponse(PostDto post) : this(post.Id, post.Title, post.Content, post.CreatedAt) { }
}