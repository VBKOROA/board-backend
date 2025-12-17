using BoardApi.Models;

namespace BoardApi.Dtos;

public record CreatePostResponse(int Id, string Title, string Content, DateTime CreatedAt)
{
    public CreatePostResponse(Post post) : this(post.Id, post.Title, post.Content, post.CreatedAt) {}
}