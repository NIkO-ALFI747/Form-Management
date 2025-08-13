using Form_Management.Application.Interfaces.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace Forms_Management.Infrastructure.Auth.ServiceCollectionExtensions;

public static class AuthServices
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        return services;
    }
}