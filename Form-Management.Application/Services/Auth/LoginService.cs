using CSharpFunctionalExtensions;
using Form_Management.Application.Contracts.Auth.Login;
using Form_Management.Application.Interfaces.Auth;
using Form_Management.Application.Interfaces.Services.Auth;
using Form_Management.Application.Interfaces.Services.Users;
using Form_Management.Domain.Errors.Error;

namespace Form_Management.Application.Services.Auth;

public class LoginService(IUsersService usersService,IPasswordHasher passwordHasher,
    IJwtProvider jwtProvider) : ILoginService
{
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    private readonly IUsersService _usersService = usersService;

    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<Result<string, AbstractError>> Login(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersService.GetByEmailAsync(request.Email, cancellationToken);
        if (user.IsFailure) return user.Error;
        var verifyingResult = _passwordHasher.Verify(request.Password, user.Value.Password.Value, "Failed to login! Provided password is incorrect!");
        if (verifyingResult.IsFailure) return verifyingResult.Error;
        var token = _jwtProvider.GenerateToken(user.Value);
        return token;
    }
}
