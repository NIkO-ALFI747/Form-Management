using Form_Management.Api.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Form_Management.Api.Extensions.ApiAuthentication;

public static class ApiAuthentication
{
    public static void AddApiAuthentication(this IServiceCollection services)
    {
        var jwtOptions = services.ConfigureJwtOptions();
        services.AddApiAuthentication(jwtOptions);
    }

    public static void AddApiAuthentication(
        this IServiceCollection services,
        JwtOptions jwtOptions
    )
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.ConfigureOptions(jwtOptions);
                }
            );
        services.AddAuthorization();
    }

    private static void ConfigureOptions(
        this JwtBearerOptions options,
        JwtOptions jwtOptions
    )
    {
        options.ConfigureTokenValidationParameters(jwtOptions);
        options.ConfigureOptionsEvents();
    }

    private static void ConfigureTokenValidationParameters(
        this JwtBearerOptions options,
        JwtOptions jwtOptions
    )
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = GetSecretKey(jwtOptions)
        };
    }

    private static SymmetricSecurityKey GetSecretKey(JwtOptions jwtOptions)
    {
        return new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtOptions.SecretKey)
        );
    }

    private static void ConfigureOptionsEvents(this JwtBearerOptions options)
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["forms-cookies"];
                return Task.CompletedTask;
            }
        };
    }
}