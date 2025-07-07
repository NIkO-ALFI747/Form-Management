using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Form_Management.Persistence.DataProtection;

public static class ServiceCollectionExtensions
{
    public static IDataProtectionBuilder AddDataProtectionPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiDbContext<DataProtectionKeyDbContext>(configuration);
        return services.AddDataProtection()
            .PersistKeysToDbContext<DataProtectionKeyDbContext>();
    }
}