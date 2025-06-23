using Form_Management.Api.Infrastructure;

namespace Form_Management.Api.Extensions.ApiAuthentication;

public static class JwtOptionsConfiguration
{
    public static JwtOptions ConfigureJwtOptions(
        this IServiceCollection services
    )
    {
        var secretKey = ValidateJwtSecretKey(services);
        var expiresHours = ValidateJwtExpiresHours(services);
        var jwtOptions = CreateJwtOptions(expiresHours, secretKey);
        services.ConfigureJwtOptions(expiresHours, secretKey);
        return jwtOptions;
    }

    private static string ValidateJwtSecretKey(IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        return ValidateSecretKey(configuration["JWT_SECRET_KEY"]);
    }

    private static string ValidateSecretKey(string? secretKey)
    {
        if (string.IsNullOrEmpty(secretKey))
            throw new Exception("JWT_SECRET_KEY is not configured");
        if (secretKey.Length < 64)
            throw new Exception("JWT_SECRET_KEY must have more than 63 symbols");
        return secretKey;
    }

    private static int ValidateJwtExpiresHours(IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        return ValidateExpiresHours(configuration["JWT_EXPIRES_HOURS"]!);
    }

    private static int ValidateExpiresHours(string expiresHoursStr)
    {
        if (!int.TryParse(expiresHoursStr, out int expiresHours) || (expiresHours <= 0))
            throw new Exception("JWT_EXPIRES_HOURS is invalid");
        return expiresHours;
    }

    private static JwtOptions CreateJwtOptions(int expiresHours, string secretKey)
    {
        return new JwtOptions
        {
            ExpiresHours = expiresHours,
            SecretKey = secretKey
        };
    }

    private static void ConfigureJwtOptions(
        this IServiceCollection services,
        int expiresHours,
        string secretKey
    )
    {
        services.Configure<JwtOptions>(options =>
        {
            options.SecretKey = secretKey;
            options.ExpiresHours = expiresHours;
        });
    }
}