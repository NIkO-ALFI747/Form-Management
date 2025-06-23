namespace Form_Management.Api.Interfaces.Infrastructure;

public interface IPasswordHasher
{
    string Generate(string password);
    bool Verify(string password, string hashedPassword);
}