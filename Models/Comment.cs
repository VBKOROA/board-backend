namespace BoardApi.Models
{
    public class Comment
    {
        public int Id {get; set;}
        public required string Contents {get; set;}
        public DateTime CreatedAt {get; init;} = DateTime.Now;

        public int PostId {get; set;}
        public required Post Post {get; set;}
    }
}