using System.Net;
using System.Text.Json;

namespace competex_backend.Common.ErrorHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiException ex) 
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, ApiException exception)
        {
            var result = JsonSerializer.Serialize(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;
            return context.Response.WriteAsync(result);
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // Default to 500 if unexpected
            var result = JsonSerializer.Serialize(new { error = "An unexpected error occurred." });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            context.Response.WriteAsync(result);
            throw exception;
        }
    }

}
