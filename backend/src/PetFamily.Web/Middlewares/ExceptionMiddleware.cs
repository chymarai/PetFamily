﻿using PetFamily.Core.Models;
using PetFamily.SharedKernel;

namespace PetFamily.Web.Middlewares;

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
            await _next(context); //вызов всего приложения
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            var error = Error.Failure("server.internal", ex.Message); //формируем ошибку
            var envelope = Envelope.Error(error);

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
