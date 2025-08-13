using CSharpFunctionalExtensions;
using Form_Management.Application.Contracts.Auth.SignUp;
using Form_Management.Application.Interfaces.Auth;
using Form_Management.Application.Interfaces.Services.Auth;
using Form_Management.Application.Interfaces.Services.Users;
using Form_Management.Domain.Errors.Error;

namespace Form_Management.Application.Services.Auth;

public class SignUpService(IUsersService usersService, IJwtProvider jwtProvider) : ISignUpService
{
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    private readonly IUsersService _usersService = usersService;

    public async Task<Result<string, AbstractError>> SignUp(SignUpUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersService.AddAsync(request, cancellationToken);
        if (user.IsFailure) return user.Error;
        var token = _jwtProvider.GenerateToken(user.Value);
        return token;
    }
}