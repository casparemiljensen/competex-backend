namespace competex_backend.Common.ErrorHandling
{
    public class ApiException : Exception
    {
        public int StatusCode;

        public ApiException(int statusCode, string? message = null) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
