using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Form_Management.Api.Extensions;

public static class ApiCors
{
    public static void AddApiCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.ConfigurePolicy(services);
            });
        });
    }

    private static void ConfigurePolicy(this CorsPolicyBuilder policy, IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        policy.WithOrigins(configuration["CORS_ALLOWED_ORIGINS"] ?? "");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    }
}