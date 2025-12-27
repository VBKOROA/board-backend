using BoardApi.Models;

namespace BoardApi.Dtos
{
    public record PostDto(int Id, string Title, string Content, DateTime CreatedAt)
    {
        public PostDto(Post post) : this(post.Id, post.Title, post.Content, post.CreatedAt) {}
    }
}