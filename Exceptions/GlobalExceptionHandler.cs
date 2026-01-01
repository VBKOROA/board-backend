using System.Net;
using BoardApi.Enums;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BoardApi.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (statusCode, title, message) = exception switch
            {
                BusinessException bx => (bx.ErrorCode.GetStatusCode(), bx.ErrorCode.GetTitle(), bx.ErrorCode.GetMessage()),
                _ => (HttpStatusCode.InternalServerError, "Internal Server Error", exception.Message)
            };

            var problemDetails = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = title,
                Detail = message,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            };

            httpContext.Response.StatusCode = (int)statusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}