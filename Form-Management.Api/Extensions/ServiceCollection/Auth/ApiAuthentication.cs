using Form_Management.Api.Extensions.ServiceCollection.Exceptions;
using Forms_Management.Infrastructure.Auth.ServiceCollectionExtensions;

namespace Form_Management.Api.Extensions.ServiceCollection.Auth;

public static class ApiAuthentication
{
    public const string JwtOptionsConfigurationErrorMessage = "Application startup configuration failed due to JWT options: {0} - {1}";

    public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration, string JwtOptionsConfigurationErrorMessagee = JwtOptionsConfigurationErrorMessage)
    {
        var jwtOptions = services.ConfigureJwtOptions(configuration);
        if (jwtOptions.IsFailure)
            throw new ConfigurationInitializationException(jwtOptions.Error, string.Format(JwtOptionsConfigurationErrorMessagee, jwtOptions.Error.Code, jwtOptions.Error.Message));
        services.AddJwtBearerAuthentication(jwtOptions.Value, configuration, AuthCookies.Key, AuthCookies.ConfigurationKey);
        services.AddAuthServices();
        services.AddAuthorizationBuilder()
            .AddPolicy("AdminPolicy", policy =>
            {
                policy.RequireClaim("Admin", "true");
            });
    }
}