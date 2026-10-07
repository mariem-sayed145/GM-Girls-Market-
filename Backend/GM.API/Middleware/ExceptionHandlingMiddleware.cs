using FluentValidation;
using System.Text.Json;

namespace GM.API.Middleware;

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
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var errors = ex.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group
                        .Select(error => error.ErrorMessage)
                        .ToArray());

            var response = new
            {
                message = "Validation failed.",
                errors
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
        catch (UnauthorizedAccessException ex)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var response = new
            {
                message = ex.Message
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
        catch (KeyNotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = "application/json";

            var response = new
            {
                message = ex.Message
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
        catch (InvalidOperationException ex)
        {
            var isConflict = ex.Message.Contains("already exists", StringComparison.OrdinalIgnoreCase) ||
                             ex.Message.Contains("conflict", StringComparison.OrdinalIgnoreCase);

            context.Response.StatusCode = isConflict
                ? StatusCodes.Status409Conflict
                : StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var response = new
            {
                message = ex.Message
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
        catch (Exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                message = "An unexpected error occurred."
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}