using Form_Management.Domain.AuthEnums;

namespace Form_Management.Application.Interfaces.Services.Auth;

public interface IPermissionService
{
    Task<HashSet<Permission>> GetPermissionsAsync(long userId, CancellationToken cancellationToken);
}