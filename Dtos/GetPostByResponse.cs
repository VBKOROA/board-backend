namespace BoardApi.Dtos;

public record GetPostByResponse(int Id, string Title, string Content, DateTime CreatedAt)
{
    public GetPostByResponse(PostDto post) : this(post.Id, post.Title, post.Content, post.CreatedAt) {}
}