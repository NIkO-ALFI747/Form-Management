using CSharpFunctionalExtensions;
using Form_Management.Domain.Errors.Validation;
using TJwtOptions = Forms_Management.Infrastructure.Auth.JwtOptions.JwtOptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forms_Management.Infrastructure.Auth.ServiceCollectionExtensions;

public static class JwtOptionsConfiguration
{
    private const string JWT_SECRET_KEY_ENV_NAME = "JWT_SECRET_KEY";

    private const string JWT_EXPIRES_HOURS_ENV_NAME = "JWT_EXPIRES_HOURS";

    public static Result<TJwtOptions, ValueObjectValidationError> ConfigureJwtOptions(
        this IServiceCollection services, IConfiguration configuration
    )
    {
        var jwtOptionsResult = CreateJwtOptions(configuration);
        if (jwtOptionsResult.IsSuccess)
        {
            services.Configure<TJwtOptions>(options =>
            {
                options.SecretKeySetter = configuration[JWT_SECRET_KEY_ENV_NAME];
                options.ExpiresHoursSetter = configuration[JWT_EXPIRES_HOURS_ENV_NAME];
            });
        }
        return jwtOptionsResult;
    }

    private static Result<TJwtOptions, ValueObjectValidationError> CreateJwtOptions(IConfiguration configuration)
    {
        return TJwtOptions.Create(configuration[JWT_SECRET_KEY_ENV_NAME], configuration[JWT_EXPIRES_HOURS_ENV_NAME]);
    }
}