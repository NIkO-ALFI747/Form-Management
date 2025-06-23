using Form_Management.Api.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Form_Management.Api.Extensions;

public static class ApiMigrations
{
    public static async void UseApiMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        await UseDataProtectionMigrations(scope);
        await UseFormManagementMigrations(scope);
    }

    private static async Task UseDataProtectionMigrations(IServiceScope scope)
    {
        await using var dataProtectionDbContext = scope.ServiceProvider.GetRequiredService<DataProtectionKeyDbContext>();
        dataProtectionDbContext?.Database.Migrate();
    }

    private static async Task UseFormManagementMigrations(IServiceScope scope)
    {
        await using var formsDbContext = scope.ServiceProvider.GetRequiredService<FormManagementDbContext>();
        formsDbContext?.Database.Migrate();
    }
}