using System.Net;
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

private readonly CookieService _cookieService;

public AuthController(AuthService authService, CookieService cookieService)
    {
        _authService = authService;
        _cookieService = cookieService;
    }

[HttpPost("login")]
public async Task<IActionResult> Login(UserLoginDto dto)
    {
        
        var tokensData = await _authService.Login(dto);

        
        if(tokensData == null)
        {
            return Unauthorized("Incorrect data");
        }

        var accessToken = tokensData.AccessToken;

        var refreshToken = tokensData.RefreshToken;

        _cookieService.SetRefreshTokenCookie(Response, refreshToken);

        return Ok(new
        {
            accessToken
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

[HttpPost("refresh")]

public async Task<IActionResult> Refresh(RefreshTokenRequestDto request)
    {
        var primalRefreshToken = _cookieService.GetRefreshToken(Request);

        if (primalRefreshToken == null)
        {
            return Unauthorized("Invalid refresh token");
        }

        var requestResponse = await _authService.Refresh(primalRefreshToken);

        if (requestResponse == null)
        {
            return Unauthorized("Invalid refresh token");
        }

        var accessToken = requestResponse.AccessToken;

        var refreshToken = requestResponse.RefreshToken;

        _cookieService.SetRefreshTokenCookie(Response, refreshToken);

        return Ok(new
        {
            accessToken
        });
    }
}