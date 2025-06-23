using Microsoft.EntityFrameworkCore;

namespace Form_Management.Api.Extensions;

public static class ApiDbContext
{
    public static void AddApiDbContext<T>(this IServiceCollection services)
        where T : DbContext
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        services.AddDbContext<T>(
            options => options.UseNpgsql(configuration["DATASOURCE_URL"])
        );
    }
}