using Microsoft.AspNetCore.Mvc;

using test_ASPNET_api.DTOs;
using test_ASPNET_api.Services;

namespace test_ASPNET_api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserAuthController : ControllerBase
{
    
private readonly UserAuthService _userAuthService;

public UserAuthController(UserAuthService userAuthService)
    {
        _userAuthService = userAuthService;
    }

[HttpGet]

public IActionResult GetUsers()
    {
        var users = _userAuthService.GetUsers();

        return Ok(users);
    }

[HttpGet("{id}")]

public IActionResult GetUser(int id)
    {
        var user = _userAuthService.GetUser(id);

        if ( user == null )
        {
            return NotFound();
        }
        else 
        return Ok(user);
    }

[HttpPost]

public IActionResult CreateUser(CreateUserDto dto)
    {
        var user = _userAuthService.CreateUser(dto);

        return Created($"api/users/{user.Id}", user);
    }
}