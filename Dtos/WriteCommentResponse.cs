namespace BoardApi.Dtos
{
    public record WriteCommentResponse(int Id, string Contents, DateTime CreatedAt)
    {
        public WriteCommentResponse(CommentDto comment) : this(comment.Id, comment.Contents, comment.CreatedAt) {}
    }
}