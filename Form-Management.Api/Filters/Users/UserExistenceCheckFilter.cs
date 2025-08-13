using CSharpFunctionalExtensions;
using Form_Management.Api.Extensions.ServiceCollection.Auth;
using Form_Management.Application.Interfaces.Services.Auth;
using Form_Management.Domain.Errors.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace Form_Management.Api.Filters.Users;

public class UserExistenceCheckFilter(ILogger<UserExistenceCheckFilter> logger) : IAsyncActionFilter
{
    private readonly ILogger<UserExistenceCheckFilter> _logger = logger;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        string cookieKey = GetAuthCookieKey(context);
        if (context.HttpContext.Request.Cookies.TryGetValue(cookieKey, out string? tokenValue) && !string.IsNullOrEmpty(tokenValue))
        {
            await HandleAuthenticatedRequest(context, tokenValue, cookieKey);
        }
        else
        {
            _logger.LogDebug("No auth cookie '{CookieKey}' found. Proceeding with action.", cookieKey);
        }
        if (context.Result == null) await next();
    }

    private static string GetAuthCookieKey(ActionExecutingContext context)
    {
        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        return configuration[AuthCookies.ConfigurationKey] ?? AuthCookies.Key;
    }

    private async Task HandleAuthenticatedRequest(ActionExecutingContext context, string tokenValue, string cookieKey)
    {
        if (TryParseUserIdFromToken(tokenValue, out long userId))
        {
            await CheckUserExistenceAndSignOut(context, userId);
        }
        else
        {
            _logger.LogWarning("Could not extract valid user ID from JWT token. Token might be malformed or missing 'userId' claim. Deleting cookie '{CookieKey}'.", cookieKey);
            context.HttpContext.Response.Cookies.Delete(cookieKey);
        }
    }

    private bool TryParseUserIdFromToken(string tokenValue, out long userId)
    {
        userId = 0;
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var userIdClaim = handler.ReadJwtToken(tokenValue).Claims.FirstOrDefault(c => c.Type == "userId");
            return userIdClaim != null && long.TryParse(userIdClaim.Value, out userId);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid JWT token format. Token: '{TokenValue}'", tokenValue);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while parsing JWT token.");
            return false;
        }
    }

    private async Task CheckUserExistenceAndSignOut(ActionExecutingContext context, long userId)
    {
        var userExistenceService = GetSignOutService(context.HttpContext);
        var doesUserExistResult = await CheckUserExistence(userExistenceService, userId, context.HttpContext);
        if (doesUserExistResult.IsFailure) HandleUserCheckFailure(doesUserExistResult.Error);
        else if (!doesUserExistResult.Value) HandleUserCheckSuccess(context, userId);
    }

    private static ISignOutService GetSignOutService(HttpContext httpContext)
    {
        return httpContext.RequestServices.GetRequiredService<ISignOutService>();
    }

    private static async Task<Result<bool, Error>> CheckUserExistence(ISignOutService userExistenceService, long userId, HttpContext httpContext)
    {
        return await userExistenceService.CheckUserAndSignOutIfNotExistAsync(
            userId, AuthCookies.Key, AuthCookies.ConfigurationKey, httpContext, httpContext.RequestAborted);
    }

    private static void HandleUserCheckFailure(Error error)
    {
        throw new Exception($"Failed to check user and sign out if not exist: {error.Message}");
    }

    private void HandleUserCheckSuccess(ActionExecutingContext context, long userId)
    {
        _logger.LogInformation("User with ID {UserId} does not exist in DB. Authentication cookie deleted.", userId);
        if (IsAuthorizationRequiredForEndpoint(context))
        {
            _logger.LogInformation("Action/Controller requires authorization. Setting Unauthorized result.");
            context.Result = new UnauthorizedResult();
        }
        else _logger.LogInformation("Action/Controller does not require authorization. Proceeding as guest (unauthenticated).");
    }

    private static bool IsAuthorizationRequiredForEndpoint(ActionExecutingContext context)
    {
        return context.ActionDescriptor.EndpointMetadata.Any(metadata => metadata is IAuthorizeData);
    }
}