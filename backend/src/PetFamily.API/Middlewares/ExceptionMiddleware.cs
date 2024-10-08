using PetFamily.API.Response;
using System.Globalization;

namespace PetFamily.API.Middlewares;

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
            await _next(context); //вызов всего приложения
        }
        catch (Exception ex)
        {
            var responseError = new ResponseError("server.internal", ex.Message, null); //формируем ошибку
            var envelope = Envelope.Error([responseError]);

            context.Response.ContentType = "application/json";  //возвращаем ответ в json
            context.Response.StatusCode = StatusCodes.Status500InternalServerError; 

            await context.Response.WriteAsJsonAsync(envelope);
        }
    }
}

public static class ExceptionMiddlewareExtensions //
{
    public static IApplicationBuilder UseExceptionMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}
