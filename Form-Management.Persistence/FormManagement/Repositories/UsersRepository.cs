using CSharpFunctionalExtensions;
using Form_Management.Domain.Errors.Error;
using Form_Management.Domain.Errors.ErrorCodes;
using Form_Management.Domain.Interfaces.Repositories.IUsersRepository;
using Form_Management.Domain.Models.User;
using Form_Management.Domain.Models.ValueObjects;
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

    public async Task<Result<bool, Error>> AddAsync(User user, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (DbUpdateException ex)
        when (ex.InnerException is PostgresException postgresEx
            && postgresEx.SqlState == UniqueViolationCode)
        {
            if (postgresEx.ConstraintName == _context.UsersEmailIndex)
                return Result.Failure<bool, Error>(Error.Conflict(
                    DbErrorCode.ValueAlreadyExist.Value, "User with the provided email already exists!"));
            return Result.Failure<bool, Error>(Error.Conflict(
                    DbErrorCode.ValueAlreadyExist.Value, $"Duplication of the field with {postgresEx.ConstraintName} constraint!"));
        }
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Users.AsNoTracking().ToListAsync(cancellationToken) ?? [];
    }

    public async Task<Result<User, Error>> GetByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        return await GetUserByPredicateAsync(u => u.Email == email,
            "User with the provided email doesn't exist!", cancellationToken);
    }

    public async Task<Result<User, Error>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await GetUserByPredicateAsync(u => u.Id == id,
            $"User with ID '{id}' doesn't exist!", cancellationToken);
    }

    private async Task<Result<User, Error>> GetUserByPredicateAsync(Expression<Func<User, bool>> predicate,
        string notFoundMessage, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(predicate, cancellationToken);
            if (user == null)
                return Result.Failure<User, Error>(Error.NotFound(DbErrorCode.RecordsNotFound.Value, notFoundMessage));
            return user;
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
}