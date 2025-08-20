using FluentValidation;
using Form_Management.Application.Contracts.Auth.SignUp;
using Form_Management.Application.Interfaces.Services.Auth;
using Form_Management.Application.Interfaces.Services.Users;
using Form_Management.Application.Interfaces.Services.ValueObjects.Password;
using Form_Management.Application.Services.Auth;
using Form_Management.Application.Services.Users;
using Form_Management.Application.Services.ValueObjects.Password;
using Microsoft.Extensions.DependencyInjection;

namespace Form_Management.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<ISignUpService, SignUpService>();
        services.AddScoped<ISignOutService, SignOutService>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IValidator<SignUpUserRequest>, SignUpUserRequestValidator>();
        services.AddScoped<IPermissionService, PermissionService>();
        return services;
    }
}