using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;

using test_ASPNET_api.DTOs;
using test_ASPNET_api.Services;

namespace test_ASPNET_api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    
private readonly AuthService _authService;

public AuthController(AuthService authService)
    {
        _authService = authService;
    }

[HttpPost("login")]
public async Task<IActionResult> Login(UserLoginDto dto)
    {
        
        var token = await _authService.Login(dto);

        if(token == null)
        {
            return Unauthorized("Incorrect data");
        }

        return Ok(new
        {
            token
        });

    }

[HttpPost("register")]
public async Task<IActionResult> Register(UserDataDto dto)
    {
        var result = await _authService.Register(dto);

        if(!result)
        {
            return BadRequest("User already exists");
        }

        return Ok("Registration is successful");
    }
}