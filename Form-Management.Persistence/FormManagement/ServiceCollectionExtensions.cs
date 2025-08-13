using Form_Management.Domain.Interfaces.Repositories.IUsersRepository;
using Form_Management.Persistence.FormManagement.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Form_Management.Persistence.FormManagement;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFormManagementPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiDbContext<FormManagementDbContext>(configuration);
        services.AddScoped<IUsersRepository, UsersRepository>();
        return services;
    }
}