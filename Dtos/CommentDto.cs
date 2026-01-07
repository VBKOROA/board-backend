using BoardApi.Models;

namespace BoardApi.Dtos
{
    public record CommentDto(int Id, string Contents, DateTime CreatedAt)
    {
        public CommentDto(Comment comment) : this(comment.Id, comment.Contents, comment.CreatedAt) {}
    }
}