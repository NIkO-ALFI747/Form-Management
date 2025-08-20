using CSharpFunctionalExtensions;
using Form_Management.Domain.AuthEnums;
using Form_Management.Domain.Errors.Error;
using Form_Management.Domain.Models.User;
using Form_Management.Domain.Models.ValueObjects;

namespace Form_Management.Domain.Interfaces.Repositories.IUsersRepository;

public interface IUsersRepository
{
    Task<Result<User, Error>> AddAsync(User user, Role role, CancellationToken cancellationToken);

    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);

    Task<Result<User, Error>> GetByEmailAsync(Email email, CancellationToken cancellationToken);

    Task<Result<User, Error>> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<Result<bool, Error>> DeleteMultipleAsync(long[] ids, CancellationToken cancellationToken);

    Task<HashSet<Permission>> GetUserPermissions(long userId, CancellationToken cancellationToken);
}