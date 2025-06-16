using Form_Management.Api.Contracts;
using Form_Management.Api.Interfaces.Repositories;
using Form_Management.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Form_Management.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<User>> GetUsers(IUsersRepository usersRepository)
    {
        return await usersRepository.GetAll();
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(CreateUserRequest request, IUsersRepository usersRepository)
    {
        var user = new User(request.Name, request.Email, request.Password);
        await usersRepository.Add(user);
        return CreatedAtAction("GetUsers", user);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id, IUsersRepository usersRepository)
    {
        await usersRepository.Delete(id);
        return NoContent();
    }
}