using CSharpFunctionalExtensions;
using Form_Management.Domain.AuthEnums;
using Form_Management.Domain.Errors.Error;
using Form_Management.Domain.Errors.ErrorCodes;
using Form_Management.Domain.Interfaces.Repositories.IUsersRepository;
using Form_Management.Domain.Models.User;
using Form_Management.Domain.Models.ValueObjects;
using Form_Management.Persistence.FormManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Linq.Expressions;

namespace Form_Management.Persistence.FormManagement.Repositories;

public class UsersRepository(FormManagementDbContext context, ILogger<UsersRepository> logger) : IUsersRepository
{
    private readonly FormManagementDbContext _context = context;

    private readonly ILogger<UsersRepository> _logger = logger;

    private readonly string UniqueViolationCode = "23505";

    public async Task<Result<User, Error>> AddAsync(User user, Role role, CancellationToken cancellationToken)
    {
        try
        {
            var createdUserEntity = await PerformAddOperation(user, role, cancellationToken);
            return User.Create(createdUserEntity.Name, createdUserEntity.Email, createdUserEntity.Password, createdUserEntity.Id).Value;
        }
        catch (DbUpdateException ex)
        {
            return HandleDbUpdateException(ex);
        }
    }

    private async Task<UserEntity> PerformAddOperation(User user, Role role, CancellationToken cancellationToken)
    {
        var roleEntity = await _context.Roles
            .SingleOrDefaultAsync(r => r.Id == (int)role, cancellationToken)
            ?? throw new InvalidOperationException($"{role} role not found.");
        var userEntity = new UserEntity()
        {
            Name = user.Name.Value,
            Email = user.Email.Value,
            Password = user.Password.Value,
            Roles = [roleEntity]
        };
        await _context.Users.AddAsync(userEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return userEntity;
    }

    private Result<User, Error> HandleDbUpdateException(DbUpdateException ex)
    {
        if (ex.InnerException is PostgresException postgresEx && postgresEx.SqlState == UniqueViolationCode)
            return HandleUniqueViolation(postgresEx);
        else throw ex;
    }
    
    private Result<User, Error> HandleUniqueViolation(PostgresException postgresEx)
    {
        if (postgresEx.ConstraintName == _context.UsersEmailIndex)
            return Result.Failure<User, Error>(Error.Conflict(
                DbErrorCode.ValueAlreadyExist.Value, "User with the provided email already exists!"));
        return Result.Failure<User, Error>(Error.Conflict(
            DbErrorCode.ValueAlreadyExist.Value, $"Duplication of the field with {postgresEx.ConstraintName} constraint!"));
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        var userEntities = await _context.Users.AsNoTracking().ToListAsync(cancellationToken) ?? [];
        return userEntities.Select(u => User.Create(u.Name, u.Email, u.Password, u.Id).Value);
    }

    public async Task<Result<User, Error>> GetByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        return await GetUserByPredicateAsync(u => u.Email == email.Value,
            "User with the provided email doesn't exist!", cancellationToken);
    }

    public async Task<Result<User, Error>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await GetUserByPredicateAsync(u => u.Id == id,
            $"User with ID '{id}' doesn't exist!", cancellationToken);
    }

    private async Task<Result<User, Error>> GetUserByPredicateAsync(Expression<Func<UserEntity, bool>> predicate,
        string notFoundMessage, CancellationToken cancellationToken)
    {
        try
        {
            var userEntity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
            if (userEntity == null)
                return Result.Failure<User, Error>(Error.NotFound(DbErrorCode.RecordsNotFound.Value, notFoundMessage));
            return User.Create(userEntity.Name, userEntity.Email, userEntity.Password, userEntity.Id).Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting user by predicate.");
            return Result.Failure<User, Error>(Error.Failure(DbErrorCode.DatabaseError.Value,
                "An unexpected error occurred while fetching user data."));
        }
    }

    public async Task<Result<bool, Error>> DeleteMultipleAsync(long[] ids, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Users.Where(u => ids.Contains(u.Id)).ExecuteDeleteAsync(cancellationToken);
            return true;
        }
        catch (Exception)
        {
            return Result.Failure<bool, Error>(Error.NotFound(
                DbErrorCode.FailedToDelete.Value, "Failed to delete multiple users."));
        }
    }

    public async Task<HashSet<Permission>> GetUserPermissions(long userId, CancellationToken cancellationToken)
    {
        var roles = await _context.Users
            .AsNoTracking()
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.Id == userId)
            .Select(u => u.Roles)
            .ToArrayAsync(cancellationToken);
        return [.. roles
            .SelectMany(r => r)
            .SelectMany(r => r.Permissions)
            .Select(p => (Permission)p.Id)
            ];
    }
}