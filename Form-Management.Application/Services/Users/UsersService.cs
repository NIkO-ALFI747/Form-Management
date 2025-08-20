using CSharpFunctionalExtensions;
using Form_Management.Application.Contracts.Users;
using Form_Management.Application.Interfaces.Auth;
using Form_Management.Application.Interfaces.Services.Users;
using Form_Management.Domain.AuthEnums;
using Form_Management.Domain.Errors.Error;
using Form_Management.Domain.Interfaces.Repositories.IUsersRepository;
using Form_Management.Domain.Models.User;
using Form_Management.Domain.Models.ValueObjects;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Form_Management.Application.Services.Users;

public class UsersService(IUsersRepository usersRepository, IPasswordHasher passwordHasher, ILogger<UsersService> logger) : IUsersService
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    private readonly ILogger<UsersService> _logger = logger;

    public async Task<IEnumerable<GetAllUsersResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var users = await _usersRepository.GetAllAsync(cancellationToken);
        return users.Adapt<IEnumerable<GetAllUsersResponse>>();
    }

    public async Task<Result<bool, AbstractError>> DeleteMultipleAsync(long[] ids, CancellationToken cancellationToken)
    {
        var deletingResutl = await _usersRepository.DeleteMultipleAsync(ids, cancellationToken);
        if (deletingResutl.IsFailure) return deletingResutl.Error;
        return true;
    }

    public async Task<Result<User, AbstractError>> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(email);
        if (emailResult.IsFailure) return emailResult.Error;
        var user = await _usersRepository.GetByEmailAsync(emailResult.Value, cancellationToken);
        if (user.IsFailure) return user.Error;
        return user.Value;
    }

    public async Task<Result<User, Error>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(id, cancellationToken);
        if (user.IsFailure)
        {
            _logger.LogInformation("User with ID {UserId} not found in DB.", id);
            return user.Error;
        }
        return user.Value;
    }

    public async Task<Result<User, AbstractError>> AddAsync(AddUserRequest request, CancellationToken cancellationToken)
    {
        var passwordHash = _passwordHasher.Generate(request.Password);
        var user = User.Create(request.Name, request.Email, passwordHash);
        if (user.IsFailure) return user.Error;
        if (!TryGetRoleValue(request.Role, out Role role))
            throw new Exception($"'{request.Role}' is not a valid Role.");
        var createdUser = await _usersRepository.AddAsync(user.Value, role, cancellationToken);
        if (createdUser.IsFailure) return createdUser.Error;
        return createdUser.Value;
    }

    private static bool TryGetRoleValue(string roleString, out Role roleValue)
    {
        roleValue = Role.User;
        if (Enum.TryParse(roleString, true, out Role roleEnum))
        {
            roleValue = roleEnum;
            return true;
        }
        return false;
    }
}