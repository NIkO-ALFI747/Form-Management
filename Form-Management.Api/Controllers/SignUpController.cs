using Form_Management.Api.Contracts;
using Form_Management.Api.Interfaces.Services;
using Form_Management.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Form_Management.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SignUpController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<User>> SignUp(
        SignUpUserRequest request,
        ISignUpService usersService)
    {
        var user = await usersService.SignUp(request.Name, request.Email, request.Password);
        return user;
    }
}