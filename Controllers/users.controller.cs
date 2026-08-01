using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using test_ASPNET_api.DTOs;
using test_ASPNET_api.Services;


namespace test_ASPNET_api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UsersController : ControllerBase
{
private readonly UsersService _usersService;

public UsersController(UsersService usersService)
    {
        _usersService = usersService;
    }
    
[Authorize]
[HttpGet]

public async Task<IActionResult> GetUsers()
    {
        var users = await _usersService.GetUsers();

        return Ok(users);
    }

[Authorize]
[HttpGet("{id}")]

public async Task<IActionResult> GetUser(int id)
    {
        var user = await _usersService.GetUser(id);

        if ( user == null )
        {
            return NotFound();
        }
        return Ok(user);
    }

[Authorize(Roles = "ADMIN")]
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteUser(int id)
    {
        var isDeleted = await _usersService.DeleteUser(id);

        if (!isDeleted)
        {
            return NotFound();
        }
        return NoContent();

    }

[Authorize]
[HttpPatch("{id}")]

public async Task<IActionResult> UpdateUserData(int id, UserUpdateDataDto dto)
    {
        var user = await _usersService.UpdateUserData(id, dto);

        if ( user == null )
        {
            return NotFound();
        }

        return Ok(user);
    }

[Authorize(Roles = "ADMIN")]
[HttpPut("{id}")]
public async Task<IActionResult> ChangeUserData(int id, UserDataDto dto)
    {
        
       var user = await _usersService.ChangeUserData(id, dto);

       if (user == null)
        {
            return NotFound();
        }
        return Ok(user);

    }

}
