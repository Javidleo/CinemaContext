using Microsoft.Data.SqlClient;
using UseCases.Exceptions;

namespace WebApplicationMVC.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        => _next = next;

        public async Task Invoke(HttpContext httpContext, IWebHostEnvironment env)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);

            }
        }

        public async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            switch (exception)
            {
                case NotFoundException:
                    await SetException(httpContext, StatusCodes.Status404NotFound, exception.Message);
                    break;
                case NotAcceptableException:
                    await SetException(httpContext, StatusCodes.Status406NotAcceptable, exception.Message);
                    break;
                case ConflictException:
                    await SetException(httpContext, StatusCodes.Status409Conflict, exception.Message);
                    break;
                case SqlException:
                    await SetException(httpContext, StatusCodes.Status500InternalServerError, "DataBase Error");
                    break;
                default:
                    await SetException(httpContext, StatusCodes.Status500InternalServerError, exception.Message);
                    break;
            }
        }

        public static async Task SetException(HttpContext httpContext, int code, string message)
        {
            httpContext.Response.StatusCode = code;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(new ErrorDetail()
            {
                StatusCode = code,
                Message = message
            }.ToString());
        }
    }
}
