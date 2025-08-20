using Form_Management.Api.Extensions.ServiceCollection;
using Form_Management.Api.Extensions.WebApplicationExtensions;
using Form_Management.Api.Extensions.WebApplicationExtensions.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var loggerFactory = LoggerFactory.Create(loggingBuilder =>
{
    loggingBuilder.AddConfiguration(builder.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();
});
var logger = loggerFactory.CreateLogger<Program>();

builder.Services.ConfigureApiServices(logger, builder.Configuration);

var app = builder.Build();

app.UseApiErrorHandling();
app.UseApiForwardedHeaders();
app.UseApiCookiePolicy();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await app.ApplyApiMigrationsAsync();

app.Run();