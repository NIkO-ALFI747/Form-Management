using Form_Management.Api.Controllers.Auth;
using Form_Management.Application.Contracts.Users;
using Form_Management.Application.Interfaces.Services.Users;
using Form_Management.Domain.AuthEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Form_Management.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController(IUsersService usersService) : ControllerBase
{
    private readonly IUsersService _usersService = usersService;

    [HttpGet]
    [PermissionAuthorize(Permission.Read)]
    public async Task<IEnumerable<GetAllUsersResponse>> GetUsers(CancellationToken cancellationToken)
    {
        return await _usersService.GetAllAsync(cancellationToken);
    }

    [HttpDelete]
    [PermissionAuthorize(Permission.Delete)]
    public async Task<IActionResult> DeleteUsers([FromBody] long[] ids, CancellationToken cancellationToken)
    {
        if (ids.Length == 0) return BadRequest("No user IDs provided!");
        await _usersService.DeleteMultipleAsync(ids, cancellationToken);
        return NoContent();
    }
}