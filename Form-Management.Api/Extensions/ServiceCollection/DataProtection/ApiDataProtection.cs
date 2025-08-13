using Form_Management.Persistence.DataProtection;
using Microsoft.AspNetCore.DataProtection;

namespace Form_Management.Api.Extensions.ServiceCollection.DataProtection;

public static class ApiDataProtection
{
    public static void AddApiDataProtection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataProtectionPersistence(configuration)
            .ProtectKeysWithCertificate(ApiX509Certificate2.LoadAndValidateX509Certificate(configuration));
    }
}