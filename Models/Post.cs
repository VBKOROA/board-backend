namespace BoardApi.Models;

public class Post
{
    public int Id {get; set;}
    public required string Title {get; set;}
    public required string Content {get; set;}
    public ICollection<Comment> Comments {get; set;} = [];
    public DateTime CreatedAt {get; init;} = DateTime.Now;

    public Comment AddNewComment(string contents)
    {
        var comment = Comment.Of(contents, this);
        Comments.Add(comment);
        return comment;
    }
}