using CSharpFunctionalExtensions;
using Form_Management.Domain.Errors.Error;

namespace Form_Management.Application.Interfaces.Auth;

public interface IPasswordHasher
{
    string Generate(string password);

    Result<bool, Error> Verify(string password, string hashedPassword, string errorMessage =
        "Provided password doesn't match hashed password!");
}