using System.Net;
using System.Text.Json;

namespace PORTAL.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode;
            object response;

            if (exception is PORTAL.DOMAIN.Exceptions.ApplicationException appException) // Resolving naming conflict
            {
                statusCode = appException.StatusCode;
                response = new
                {
                    statusCode,
                    message = appException.Message,
                    type = appException.Title
                };
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
                response = new
                {
                    statusCode,
                    message = "An error occurred",
                    type = "Internal Server Error"
                };
            }

            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
