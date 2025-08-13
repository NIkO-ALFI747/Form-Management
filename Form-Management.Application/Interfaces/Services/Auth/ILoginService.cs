using CSharpFunctionalExtensions;
using Form_Management.Application.Contracts.Auth.Login;
using Form_Management.Domain.Errors.Error;

namespace Form_Management.Application.Interfaces.Services.Auth;

public interface ILoginService
{
    Task<Result<string, AbstractError>> Login(LoginUserRequest request, CancellationToken cancellationToken);
}