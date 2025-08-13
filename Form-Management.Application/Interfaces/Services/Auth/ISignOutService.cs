using CSharpFunctionalExtensions;
using Form_Management.Domain.Errors.Error;
using Microsoft.AspNetCore.Http;

namespace Form_Management.Application.Interfaces.Services.Auth;

public interface ISignOutService
{
    Task<Result<bool, Error>> CheckUserAndSignOutIfNotExistAsync(long userId, string AuthCookiesKey,
        string AuthCookiesConfigurationKey, HttpContext httpContext, CancellationToken cancellationToken);
}