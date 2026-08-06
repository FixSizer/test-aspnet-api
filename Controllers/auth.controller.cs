using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
        
        var tokensData = await _authService.Login(dto);

        
        if(tokensData == null)
        {
            return Unauthorized("Incorrect data");
        }

        return Ok(tokensData);

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

[HttpPost("refresh")]

public async Task<IActionResult> Refresh(RefreshTokenRequestDto request)
    {
        var requestResponse = await _authService.Refresh(request);

        if (requestResponse == null)
        {
            return Unauthorized("Invalid refresh token");
        }
        return Ok(requestResponse);
    }
}