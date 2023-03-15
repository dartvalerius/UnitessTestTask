using System.Net;
using UnitessTestTask.WebApi.Exceptions;

namespace UnitessTestTask.WebApi.Middleware;

/// <summary>
/// Обработка исключений в потоке обработки запроса
/// </summary>
public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;

        string result;

        switch (exception)
        {
            case ValidationException ex:
                code = HttpStatusCode.BadRequest;
                result = ex.Message;
                break;
            case NotFoundException ex:
                code = HttpStatusCode.NotFound;
                result = ex.Message;
                break;
            case AuthException ex:
                code = HttpStatusCode.Unauthorized;
                result = ex.Message;
                break;
            default:
                result = exception.Message;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}