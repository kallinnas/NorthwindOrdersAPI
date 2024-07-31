using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace NorthwindOrdersAPI.Data
{
    public class CentralErrorHandler
    {
        private readonly RequestDelegate _next;

        public CentralErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception error)
            {
                await HandleExceptionAsync(context, error);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = error switch
            {
                ApplicationException => StatusCodes.Status400BadRequest,
                DbUpdateException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var result = JsonSerializer.Serialize(new { message = error?.Message });
            response.StatusCode = statusCode;

            // Log detailed error information
            Console.Error.WriteLine($"{DateTime.UtcNow}: {error.Message} {error.StackTrace}");
            return response.WriteAsync(result);
        }
    }
}
