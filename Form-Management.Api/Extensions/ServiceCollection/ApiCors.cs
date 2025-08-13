using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Form_Management.Api.Extensions.ServiceCollection;

public static class ApiCors
{
    private const string CORS_ALLOWED_ORIGINS_ENV_NAME = "CORS_ALLOWED_ORIGINS";

    public static void AddApiCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.ConfigurePolicy(configuration);
            });
        });
    }

    private static void ConfigurePolicy(this CorsPolicyBuilder policy, IConfiguration configuration)
    {
        policy.WithOrigins(configuration[CORS_ALLOWED_ORIGINS_ENV_NAME] ?? "")
            .WithMethods("GET", "POST", "DELETE")
            .WithHeaders("Content-Type", "Authorization")
            .AllowCredentials();
    }
}