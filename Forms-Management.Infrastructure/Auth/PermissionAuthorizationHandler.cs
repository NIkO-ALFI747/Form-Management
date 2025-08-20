using Form_Management.Application.Interfaces.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Forms_Management.Infrastructure.Auth;

public class PermissionAuthorizationHandler(
    IServiceScopeFactory serviceScopeFactory,
    IHttpContextAccessor httpContextAccessor)
    : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
    
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId);
        if (userId is null || !long.TryParse(userId.Value, out var id)) return;
        var cancellationToken = _httpContextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None;
        using var scope = _serviceScopeFactory.CreateScope();
        var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
        var permissions = await permissionService.GetPermissionsAsync(id, cancellationToken);
        if (permissions.Intersect(requirement.Permissions).Any()) context.Succeed(requirement);
    }
}
