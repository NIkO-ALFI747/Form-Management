using Form_Management.Api.Interfaces.Infrastructure;
using Form_Management.Api.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Form_Management.Api.Infrastructure;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;

    public string GenerateToken(User user)
    {
        Claim[] claims = ConfigureClaims(user);
        var signingCredentials = ConfigureSigningCredentials();
        var token = ConfigureToken(claims, signingCredentials);
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenValue;
    }

    private static Claim[] ConfigureClaims(User user)
    {
        return [new("userId", user.Id.ToString())];
    }

    private SigningCredentials ConfigureSigningCredentials()
    {
        return new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256
        );
    }

    private JwtSecurityToken ConfigureToken(
        Claim[] claims,
        SigningCredentials signingCredentials
    )
    {
        return new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_options.ExpiresHours)
        );
    }
}
