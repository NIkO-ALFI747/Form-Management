using Form_Management.Application.Interfaces.Auth;
using Form_Management.Domain.Models.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TJwtOptions = Forms_Management.Infrastructure.Auth.JwtOptions.JwtOptions;

namespace Forms_Management.Infrastructure.Auth;

public class JwtProvider(IOptions<TJwtOptions> options) : IJwtProvider
{
    private readonly TJwtOptions _options = options.Value;

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
        return [
            new(CustomClaims.UserId, user.Id.ToString())
        ];
    }

    private SigningCredentials ConfigureSigningCredentials()
    {
        return new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey!.Value)),
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
            expires: DateTime.UtcNow.AddHours(_options.ExpiresHours!.Value)
        );
    }
}
