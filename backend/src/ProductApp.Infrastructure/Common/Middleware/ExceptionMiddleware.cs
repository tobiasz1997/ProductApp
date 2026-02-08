using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProductApp.Core.Common.Exceptions.Abstractions;

namespace ProductApp.Infrastructure.Common.Middleware;

internal sealed class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (System.Exception exception)
        {
            logger.LogError(exception, exception.Message);
            await HandleExceptionAsync(exception, context);
        }
    }

    private static async Task HandleExceptionAsync(System.Exception exception, HttpContext context)
    {
        var (statusCode, error) = exception switch
        {
            BadRequestException => (StatusCodes.Status400BadRequest, exception.Message),
            UnauthorizedException => (StatusCodes.Status401Unauthorized, exception.Message),
            NotFoundException => (StatusCodes.Status404NotFound, exception.Message),
            ConflictException => (StatusCodes.Status409Conflict, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
        };
    
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }
}