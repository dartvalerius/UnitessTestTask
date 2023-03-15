namespace UnitessTestTask.WebApi.Middleware;

/// <summary>
/// Расширения для Middleware
/// </summary>
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}