using CSharpFunctionalExtensions;
using Form_Management.Application.Contracts.Auth.SignUp;
using Form_Management.Domain.Errors.Error;

namespace Form_Management.Application.Interfaces.Services.Auth;

public interface ISignUpService
{
    Task<Result<string, AbstractError>> SignUp(SignUpUserRequest request, CancellationToken cancellationToken);
}