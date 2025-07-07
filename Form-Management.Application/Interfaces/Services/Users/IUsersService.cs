using CSharpFunctionalExtensions;
using Form_Management.Application.Contracts.Users;
using Form_Management.Domain.Errors.Error;
using Form_Management.Domain.Models.User;

namespace Form_Management.Application.Interfaces.Services.Users;

public interface IUsersService
{
    Task<IEnumerable<GetAllUsersResponse>> GetAllAsync(CancellationToken cancellationToken);

    Task<Result<bool, AbstractError>> DeleteMultipleAsync(long[] ids, CancellationToken cancellationToken);

    Task<Result<User, AbstractError>> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<Result<User, Error>> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<Result<User, AbstractError>> AddAsync(AddUserRequest request, CancellationToken cancellationToken);
}