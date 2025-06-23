using Form_Management.Api.Contracts;
using Form_Management.Api.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Form_Management.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    public async Task<IResult> Login(
        LoginUserRequest request,
        ILoginService usersService)
    {
        var token = await usersService.Login(request.Email, request.Password);
        HttpContext.Response.Cookies.Append("forms-cookies", token);
        return Results.Ok();
    }
}