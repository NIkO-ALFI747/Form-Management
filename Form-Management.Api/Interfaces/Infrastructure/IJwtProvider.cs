using Form_Management.Api.Models;

namespace Form_Management.Api.Interfaces.Infrastructure;

public interface IJwtProvider
{
    string GenerateToken(User user);
}