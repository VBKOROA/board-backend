using System.Net;

namespace BoardApi.Enums
{
    public enum CommonErrorCode
    {
        PostNotFound
    }

    public static class CommonErrorCodeExtension
    {
        public static HttpStatusCode GetStatusCode(this CommonErrorCode errorCode) => errorCode switch
        {
            CommonErrorCode.PostNotFound => HttpStatusCode.NotFound,
            _ => HttpStatusCode.InternalServerError
        };
    
        public static string GetMessage(this CommonErrorCode errorCode) => errorCode switch
        {
            CommonErrorCode.PostNotFound => "Post를 찾을 수 없습니다.",
            _ => "알 수 없는 오류입니다."
        };

        public static string GetTitle(this CommonErrorCode errorCode) => errorCode switch
        {
            CommonErrorCode.PostNotFound => "Post not found",
            _ => "Unknown exception"
        };
    }
}