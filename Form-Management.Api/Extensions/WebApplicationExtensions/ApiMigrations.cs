using Form_Management.Persistence.DataProtection;
using Form_Management.Persistence.FormManagement;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Npgsql;

namespace Form_Management.Api.Extensions.WebApplicationExtensions;

public static class ApiMigrations
{
    public static async Task ApplyApiMigrationsAsync(this WebApplication app)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        await ExecuteMigrationStrategyAsync(app.Services, logger);
    }

    private static async Task ExecuteMigrationStrategyAsync(IServiceProvider rootServices, ILogger logger)
    {
        const int maxRetries = 5;
        var delay = TimeSpan.FromSeconds(2);
        for (var attempt = 1; attempt <= maxRetries; attempt++)
        {
            using (var scope = rootServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                if (await TryMigrationAttemptAsync(services, logger, attempt, maxRetries, delay)) return;
            }
            delay *= 2;
        }
    }

    private static async Task<bool> TryMigrationAttemptAsync(IServiceProvider services, ILogger logger, int attempt, int maxRetries, TimeSpan delay)
    {
        try
        {
            logger.LogInformation("Applying migrations (Attempt {Num}/{Max})", attempt, maxRetries);
            await ApplyAllMigrationsAsync(services, logger);
            return true;
        }
        catch (DbException ex)
        {
            if (ex is PostgresException pgEx && pgEx.SqlState == "42P07")
            {
                logger.LogError(pgEx, "Migration failed: Table 'DataProtectionKeys' already exists. Aborting migration process.");
                return true;
            }
            await HandleMigrationFailureAsync(ex, logger, attempt, maxRetries, delay);
            return false;
        }
    }

    private static async Task ApplyAllMigrationsAsync(IServiceProvider services, ILogger logger)
    {
        await MigrateDbContextAsync<DataProtectionKeyDbContext>(services, logger);
        await MigrateDbContextAsync<FormManagementDbContext>(services, logger);
        logger.LogInformation("All database migrations applied successfully.");
    }

    private static async Task MigrateDbContextAsync<TContext>(IServiceProvider services, ILogger logger) where TContext : DbContext
    {
        var contextName = typeof(TContext).Name;
        logger.LogInformation("Applying migrations for {DbContextName}...", contextName);
        await using var dbContext = services.GetRequiredService<TContext>();
        await dbContext.Database.MigrateAsync();
        logger.LogInformation("{DbContextName} migrations applied successfully.", contextName);
    }

    private static async Task HandleMigrationFailureAsync(DbException ex, ILogger logger, int attempt, int maxRetries, TimeSpan delay)
    {
        if (attempt < maxRetries)
        {
            logger.LogWarning(ex, "Database connection failed. Retrying in {Delay}s...", delay.TotalSeconds);
            await Task.Delay(delay);
        }
        else
        {
            logger.LogError(ex, "Could not connect to DB after {Max} attempts.", maxRetries);
            throw ex;
        }
    }
}