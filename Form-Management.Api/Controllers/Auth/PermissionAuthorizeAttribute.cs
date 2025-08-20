using Form_Management.Domain.AuthEnums;
using Form_Management.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Form_Management.Api.Controllers.Auth;

public class PermissionAuthorizeAttribute : AuthorizeAttribute
{
    public PermissionAuthorizeAttribute(params Permission[] permissions)
    {
        Policy = AuthorizationConstants.PolicyPrefix + string.Join(",", permissions);
    }
}