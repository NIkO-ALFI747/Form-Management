using Form_Management.Api.DataAccess;
using Form_Management.Api.Extensions;
using Form_Management.Api.Extensions.ApiAuthentication;
using Form_Management.Api.Infrastructure;
using Form_Management.Api.Interfaces.Infrastructure;
using Form_Management.Api.Interfaces.Repositories;
using Form_Management.Api.Interfaces.Services;
using Form_Management.Api.Repositories;
using Form_Management.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiDataProtection();
builder.Services.AddApiDbContext<FormManagementDbContext>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ISignUpService, SignUpService>();

builder.Services.AddApiCors();
builder.Services.AddApiAuthentication();
builder.Services.AddControllers();

var app = builder.Build();

app.UseApiMigrations();
app.UseApiForwardedHeaders();
app.UseApiCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();