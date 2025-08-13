using Form_Management.Api.Contracts.Response.Error.Result.ProblemDetails;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Form_Management.Api.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;

    private readonly ILogger<ErrorHandlingMiddleware> _logger = logger;

    private readonly JsonSerializerOptions options = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleUnhandledExceptionAsync(context, ex);
        }
    }

    private async Task HandleUnhandledExceptionAsync(HttpContext context, Exception exception)
    {
        string? error = null;
        _logger.LogError(exception, "An unhandled exception has occurred.");
        var problemDetailsResult = error.ToErrorProblemDetailsResult(context, "An unexpected error occurred.", 
            StatusCodes.Status500InternalServerError, "Please try again later. If the problem persists, contact support.");
        var jsonResponse = JsonSerializer.Serialize(problemDetailsResult, options);
        await context.Response.WriteAsync(jsonResponse);
    }
}