using Form_Management.Api.Models;

namespace Form_Management.Api.Interfaces.Services;

public interface ISignUpService
{
    Task<User> SignUp(string name, string email, string password);
}