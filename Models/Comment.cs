using BoardApi.Enums;
using BoardApi.Exceptions;

namespace BoardApi.Models
{
    public class Comment
    {
        public const int ContentsMinLength = 1;
        public const int ContentsMaxLength = 255;

        public int Id {get; set;}
        public required string Contents {get; set;}
        public DateTime CreatedAt {get; init;} = DateTime.Now;

        public int PostId {get; set;}
        public required Post Post {get; set;}

        public static Comment Of(string contents, Post post)
        {
            if (string.IsNullOrWhiteSpace(contents))
            {
                throw new BusinessException(CommonErrorCode.CommentContentsEmpty);
            }

            if (contents.Length < ContentsMinLength || contents.Length > ContentsMaxLength)
            {
                throw new BusinessException(CommonErrorCode.CommentContentsLengthInvalid);
            }

            return new()
            {
              Contents = contents,
              Post = post,
              PostId = post.Id
            };
        }
    }
}