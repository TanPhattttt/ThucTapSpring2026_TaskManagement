using System.Text.Json;
using WebAPI_TT1._1.Exceptions;

namespace WebAPI_TT1._1.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new
                {
                    message = ex.Message
                });

                await context.Response.WriteAsync(result);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;

                var result = JsonSerializer.Serialize(new
                {
                    message = ex.Message
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}
