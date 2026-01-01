using BoardApi.Enums;

namespace BoardApi.Exceptions
{
    public class BusinessException(CommonErrorCode errorCode) : Exception(errorCode.GetMessage())
    {
        public CommonErrorCode ErrorCode = errorCode;
    }
}