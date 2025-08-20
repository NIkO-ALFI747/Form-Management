using Form_Management.Domain.AuthEnums;
using Microsoft.AspNetCore.Authorization;

namespace Forms_Management.Infrastructure.Auth;

public class PermissionRequirement(Permission[] permissions) 
: IAuthorizationRequirement
{
    public Permission[] Permissions { get; set; } = permissions;
}