using Form_Management.Api.Extensions.ServiceCollection.Auth;
using Form_Management.Api.Extensions.ServiceCollection.DataProtection;
using Form_Management.Api.Extensions.ServiceCollection.Exceptions;
using Form_Management.Api.Extensions.ServiceCollection.Mapping;
using Form_Management.Api.Filters.Users;
using Form_Management.Application;
using Form_Management.Persistence.FormManagement;

namespace Form_Management.Api.Extensions.ServiceCollection;

public static class ApiExtensions
{
    public static void ConfigureApiServices(this IServiceCollection services, ILogger logger, IConfiguration configuration)
    {
        try
        {
            MapsterRegistration.RegisterMapping();
            services.AddApiDataProtection(configuration);
            services.AddApiAuthentication(configuration);
            services.AddApiCors(configuration);
            services.AddFormManagementPersistence(configuration);
            services.AddApplicationServices();
            services.AddControllers(options =>
            {
                options.Filters.Add<UserExistenceCheckFilter>();
            });
        }
        catch (ConfigurationInitializationException ex)
        {
            logger.LogCritical(ex, "FATAL: {ExceptionMessage}", ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "FATAL: An unexpected error occurred during application startup: {ExceptionMessage}", ex.Message);
            throw;
        }
    }
}