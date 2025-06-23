using Form_Management.Api.Interfaces.Infrastructure;
using Form_Management.Api.Interfaces.Repositories;
using Form_Management.Api.Interfaces.Services;

namespace Form_Management.Api.Services;

public class LoginService(
    IUsersRepository usersRepository,
    IPasswordHasher passwordHasher,
    IJwtProvider jwtProvider) : ILoginService
{
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    private readonly IUsersRepository _usersRepository = usersRepository;

    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<string> Login(string email, string password)
    {
        var user = await _usersRepository.GetByEmail(email);
        var result = _passwordHasher.Verify(password, user.Password);
        if (result == false) throw new Exception("Failed to login");
        var token = _jwtProvider.GenerateToken(user);
        return token;
    }
}
