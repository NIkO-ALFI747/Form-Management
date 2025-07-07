using Form_Management.Api.Extensions.ServiceCollection.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Form_Management.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SignOutController(IConfiguration configuration) : ControllerBase
{
    private readonly IConfiguration _configuration = configuration;

    [HttpGet]
    public new IActionResult SignOut()
    {
        string cookieKey = _configuration[AuthCookies.ConfigurationKey] ?? AuthCookies.Key;
        Response.Cookies.Delete(cookieKey);
        return Ok("Signed out successfully.");
    }
}