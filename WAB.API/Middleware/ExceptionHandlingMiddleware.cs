using System.Text.Json;
using WAB.BLL.Exceptions;

namespace WAB.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        if (ex is ObjectDoesNotExistException) context.Response.StatusCode = StatusCodes.Status404NotFound;

        var result = JsonSerializer.Serialize(new
        {
            StatusCode = context.Response.StatusCode,
            Message = ex.Message
        });

        if (!string.IsNullOrEmpty(result)) await context.Response.WriteAsync(result);
    }
}