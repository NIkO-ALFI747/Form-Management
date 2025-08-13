using CSharpFunctionalExtensions;
using Form_Management.Application.Interfaces.Auth;
using Form_Management.Domain.Errors.Error;
using Form_Management.Domain.Errors.ErrorCodes.ValueObject;

namespace Forms_Management.Infrastructure.Auth;

public class PasswordHasher : IPasswordHasher
{
    public string Generate(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public Result<bool, Error> Verify(string password, string hashedPassword, string errorMessage = "Provided password doesn't match hashed password!") {
        return !BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword)
            ? Result.Failure<bool, Error>(Error.BadRequest(ValueObjectErrorCode.ValueIsInvalid.Value, errorMessage))
            : true;
    }
}
