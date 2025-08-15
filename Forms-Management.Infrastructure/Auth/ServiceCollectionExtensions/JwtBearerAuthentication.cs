using TJwtOptions = Forms_Management.Infrastructure.Auth.JwtOptions.JwtOptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Forms_Management.Infrastructure.Auth.ServiceCollectionExtensions;

public static partial class JwtBearerAuthentication
{
    public static IServiceCollection AddJwtBearerAuthentication(
        this IServiceCollection services,
        TJwtOptions jwtOptions,
        IConfiguration configuration,
        string AuthCookiesKey,
        string AuthCookiesConfigurationKey
    )
    {
        var authCookies = new AuthCookies(AuthCookiesKey, AuthCookiesConfigurationKey);
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(
                JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.ConfigureOptions(jwtOptions, configuration, authCookies);
                }
            );
        return services;
    }

    private static void ConfigureOptions(
        this JwtBearerOptions options,
        TJwtOptions jwtOptions,
        IConfiguration configuration,
        AuthCookies authCookies
    )
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.ConfigureTokenValidationParameters(jwtOptions);
        options.ConfigureOptionsEvents(configuration, authCookies);
    }

    private static void ConfigureTokenValidationParameters(
        this JwtBearerOptions options,
        TJwtOptions jwtOptions
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

    private static SymmetricSecurityKey GetSecretKey(TJwtOptions jwtOptions)
    {
        return new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtOptions.SecretKey!.Value)
        );
    }

    private static void ConfigureOptionsEvents(this JwtBearerOptions options, IConfiguration configuration, AuthCookies authCookies)
    {
        var cookiesKey = configuration[authCookies.ConfigurationKey] ?? authCookies.Key;
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies[cookiesKey];
                return Task.CompletedTask;
            }
        };
    }
}