using Form_Management.Api.DataAccess;
using Form_Management.Api.Interfaces.Repositories;
using Form_Management.Api.Repositories;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FormManagementDbContext>
    (
    options => options.UseNpgsql
        (
            Environment.GetEnvironmentVariable("DATASOURCE_URL") ??
            "DB_STRING"
        )
    );

builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins
        (
            Environment.GetEnvironmentVariable("CORS_ALLOWED_ORIGINS") ??
            "http://localhost:5000"
        );
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<FormManagementDbContext>();
dbContext?.Database.Migrate();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders =
    ForwardedHeaders.XForwardedFor |
    ForwardedHeaders.XForwardedProto
});

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();