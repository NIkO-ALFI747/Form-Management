using Form_Management.Api.Middlewares;

namespace Form_Management.Api.Extensions.WebApplicationExtensions.Middlewares;

public static class ApiErrorHandling
{
    public static void UseApiErrorHandling(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}