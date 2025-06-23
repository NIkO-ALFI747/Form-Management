namespace Form_Management.Api.Interfaces.Services;

public interface ILoginService
{
    Task<string> Login(string email, string password);
}