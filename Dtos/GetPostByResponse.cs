using BoardApi.Models;

namespace BoardApi.Dtos;

public record GetPostByResponse(int Id, string Title, string Content, DateTime CreatedAt)
{
    public GetPostByResponse(Post post) : this(post.Id, post.Title, post.Content, post.CreatedAt) {}
}