using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Form_Management.Persistence;

public static class ServiceCollectionExtensions
{
    private const string DATASOURCE_URL_ENV_NAME = "DATASOURCE_URL";

    public static IServiceCollection AddApiDbContext<T>(this IServiceCollection services, IConfiguration configuration)
        where T : DbContext
    {
        services.AddDbContext<T>(
            options => options.UseNpgsql(configuration[DATASOURCE_URL_ENV_NAME])
        );
        return services;
    }
}