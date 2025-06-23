using Form_Management.Api.Interfaces.Infrastructure;
using Form_Management.Api.Interfaces.Repositories;
using Form_Management.Api.Interfaces.Services;
using Form_Management.Api.Models;

namespace Form_Management.Api.Services;

public class SignUpService(
    IUsersRepository usersRepository,
    IPasswordHasher passwordHasher) : ISignUpService
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<User> SignUp(string name, string email, string password)
    {
        var passwordHash = _passwordHasher.Generate(password);
        var user = new User(name, email, passwordHash);
        await _usersRepository.Add(user);
        return user;
    }
}
