using System.Net;

namespace BoardApi.Enums
{
    public enum CommonErrorCode
    {
        PostNotFound,
        CommentContentsEmpty,
        CommentContentsLengthInvalid
    }

    public static class CommonErrorCodeExtension
    {
        public static HttpStatusCode GetStatusCode(this CommonErrorCode errorCode) => errorCode switch
        {
            CommonErrorCode.PostNotFound => HttpStatusCode.NotFound,
            CommonErrorCode.CommentContentsEmpty => HttpStatusCode.BadRequest,
            CommonErrorCode.CommentContentsLengthInvalid => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };
    
        public static string GetMessage(this CommonErrorCode errorCode) => errorCode switch
        {
            CommonErrorCode.PostNotFound => "Post를 찾을 수 없습니다.",
            CommonErrorCode.CommentContentsEmpty => "댓글 내용은 필수입니다.",
            CommonErrorCode.CommentContentsLengthInvalid => "댓글 길이가 적절하지 않습니다.",
            _ => "알 수 없는 오류입니다."
        };

        public static string GetTitle(this CommonErrorCode errorCode) => errorCode switch
        {
            CommonErrorCode.PostNotFound => "Post Not Found",
            CommonErrorCode.CommentContentsEmpty => "Empty Comment",
            CommonErrorCode.CommentContentsLengthInvalid => "Invalid Comment Length",
            _ => "Unknown exception"
        };
    }
}