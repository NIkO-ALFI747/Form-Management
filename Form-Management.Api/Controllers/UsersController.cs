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

    [HttpDelete]
    public async Task<ActionResult> DeleteUsers([FromBody] int[] ids, IUsersRepository usersRepository)
    {
        if (ids.Length == 0) return BadRequest("No user IDs provided!");
        await usersRepository.DeleteMultiple(ids);
        return NoContent();
    }
}