using Form_Management.Application.Interfaces.Services.Auth;
using Form_Management.Domain.AuthEnums;
using Form_Management.Domain.Interfaces.Repositories.IUsersRepository;

namespace Form_Management.Application.Services.Auth;

public class PermissionService(IUsersRepository usersRepository) : IPermissionService
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public Task<HashSet<Permission>> GetPermissionsAsync(long userId, CancellationToken cancellationToken)
    {
        return _usersRepository.GetUserPermissions(userId, cancellationToken);
    }
}
