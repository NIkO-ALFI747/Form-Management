using Form_Management.Api.Extensions.ServiceCollection.Auth;
using Form_Management.Application.Contracts.Auth.Login;
using Form_Management.Application.Interfaces.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Form_Management.Api.Controllers.Login;

[Route("api/[controller]")]
[ApiController]
public class LoginController(ILoginService loginService, IConfiguration configuration) : ControllerBase
{
    private readonly ILoginService _loginService = loginService;

    private readonly IConfiguration _configuration = configuration;

    [HttpPost]
    public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var token = await _loginService.Login(request, cancellationToken);
        if (token.IsFailure) return token.Error.MapLoginErrorsToProblemDetailsResult(HttpContext);
        HttpContext.Response.Cookies.Append(_configuration[AuthCookies.ConfigurationKey] ?? AuthCookies.Key, token.Value);
        return Ok();
    }
}