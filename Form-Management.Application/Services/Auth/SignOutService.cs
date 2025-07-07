using CSharpFunctionalExtensions;
using Form_Management.Application.Interfaces.Services.Auth;
using Form_Management.Application.Interfaces.Services.Users;
using Form_Management.Domain.Errors.Error;
using Form_Management.Domain.Errors.ErrorCodes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Form_Management.Application.Services.Auth;

public class SignOutService(IUsersService usersService, IConfiguration configuration, ILogger<SignOutService> logger) : ISignOutService
{
    private readonly IUsersService _usersService = usersService;

    private readonly IConfiguration _configuration = configuration;

    private readonly ILogger<SignOutService> _logger = logger;

    public async Task<Result<bool, Error>> CheckUserAndSignOutIfNotExistAsync(long userId, string AuthCookiesKey,
        string AuthCookiesConfigurationKey, HttpContext httpContext, CancellationToken cancellationToken)
    {
        if (httpContext == null)
            return Error.Failure(ErrorCode.HttpContextIsNull.Value, $"HttpContext is null in {nameof(CheckUserAndSignOutIfNotExistAsync)}. Cannot manage cookies.");
        var userResult = await _usersService.GetByIdAsync(userId, cancellationToken);
        if (userResult.IsSuccess) return true;
        if (userResult.IsFailure && (userResult.Error.Code == DbErrorCode.RecordsNotFound.Value))
        {
            _logger.LogInformation("Signing out by deleting authentication cookie.");
            string cookieKey = _configuration[AuthCookiesConfigurationKey] ?? AuthCookiesKey;
            httpContext.Response.Cookies.Delete(cookieKey);
            return false;
        } else return userResult.Error;
    }
}