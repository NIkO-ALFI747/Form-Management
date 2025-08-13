using CSharpFunctionalExtensions;
using Form_Management.Application.Interfaces.Services.Auth;
using Form_Management.Application.Interfaces.Services.Users;
using Form_Management.Domain.Errors.Error;
using Form_Management.Domain.Errors.ErrorCodes;
using Form_Management.Domain.Models.User;
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
        var httpContextValidationResult = ValidateHttpContext(httpContext);
        if (httpContextValidationResult.IsFailure) return httpContextValidationResult.Error;
        var userResult = await _usersService.GetByIdAsync(userId, cancellationToken);
        return CheckUserResult(userResult, AuthCookiesKey, AuthCookiesConfigurationKey, httpContext, userId);
    }
    
    private Result<bool, Error> ValidateHttpContext(HttpContext httpContext)
    {
        if (httpContext == null)
            return Error.Failure(ErrorCode.HttpContextIsNull.Value, 
                $"HttpContext is null in {nameof(CheckUserAndSignOutIfNotExistAsync)}. Cannot manage cookies.");
        return true;
    }
    
    private Result<bool, Error> CheckUserResult(Result<User, Error> userResult, string AuthCookiesKey,
        string AuthCookiesConfigurationKey, HttpContext httpContext, long userId)
    {
        if (userResult.IsSuccess) return true;
        if (userResult.IsFailure && (userResult.Error.Code == DbErrorCode.RecordsNotFound.Value))
        {
            _logger.LogInformation("User with ID {UserId} not found. Signing out by deleting authentication cookie.", userId);
            SignOutUser(AuthCookiesKey, AuthCookiesConfigurationKey, httpContext);
            return false;
        }
        else return userResult.Error;
    }

    private void SignOutUser(string AuthCookiesKey, string AuthCookiesConfigurationKey, HttpContext httpContext)
    {
        string cookieKey = _configuration[AuthCookiesConfigurationKey] ?? AuthCookiesKey;
        httpContext.Response.Cookies.Delete(cookieKey);
        _logger.LogInformation("Authentication cookie '{CookieKey}' deleted.", cookieKey);
    }
}