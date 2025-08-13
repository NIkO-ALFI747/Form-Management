using Form_Management.Domain.Models.User;

namespace Form_Management.Application.Interfaces.Auth;

public interface IJwtProvider
{
    string GenerateToken(User user);
}