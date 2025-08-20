using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Form_Management.Domain.AuthEnums;
using Form_Management.Domain.Constants;

namespace Forms_Management.Infrastructure.Auth;

public class PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : DefaultAuthorizationPolicyProvider(options)
{
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith(AuthorizationConstants.PolicyPrefix))
        {
            var parts = policyName[AuthorizationConstants.PolicyPrefix.Length..].Split(',');
            var permissions = parts
                .Select(p => Enum.TryParse<Permission>(p, out var parsedPermission) ? (Permission?)parsedPermission : null)
                .OfType<Permission>()
                .ToArray();
            var policy = new AuthorizationPolicyBuilder();
            policy.AddRequirements(new PermissionRequirement(permissions));
            return policy.Build();
        }
        return await base.GetPolicyAsync(policyName);
    }
}
