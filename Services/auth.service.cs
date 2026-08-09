using test_ASPNET_api.Models;
using test_ASPNET_api.Data;
using test_ASPNET_api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace test_ASPNET_api.Services;

public class AuthService
{
    
private readonly UsersDbContext _usersContext;
private readonly AccessTokenService _accessTokenService;

private readonly IPasswordService _passwordService;

private readonly RefreshTokenService _refreshTokenService;

public AuthService(UsersDbContext usersContext, AccessTokenService accessTokenService, 
IPasswordService passwordService, RefreshTokenService refreshTokenService)
    {
        _refreshTokenService = refreshTokenService;
        _usersContext = usersContext;
        _accessTokenService = accessTokenService;
        _passwordService = passwordService;
    }


public async Task<bool> Register(UserDataDto dto)
    {
        var exists = await _usersContext.Users.AnyAsync(x => x.Name == dto.Name);

        if(exists)
        {
            return false;
        }
        var user = new UserDataModel
        {
            Name = dto.Name,
            PasswordHash = _passwordService.HashPassword(dto.Password),
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            RegistrationDate = DateTime.UtcNow,
            Role = UserRole.USER
        };

        await _usersContext.Users.AddAsync(user);
        await _usersContext.SaveChangesAsync();

        return true;
    }

public async Task<AuthResponseDto?> Login(UserLoginDto dto)
    {
        var user = await _usersContext.Users.FirstOrDefaultAsync(x => x.Name == dto.Name);

        if(user == null)
        {
            return null;
        }

        bool passwordCorrect = _passwordService.VerifyPassword(dto.Password, user.PasswordHash);

        if(!passwordCorrect)
        {
            return null;
        }

        string accessToken = _accessTokenService.CreateAccessToken(user);

        var refreshEntity = _refreshTokenService.CreateToken(user.Id, out string refreshToken);

        await _usersContext.RefreshTokens.AddAsync(refreshEntity);

        await _usersContext.SaveChangesAsync();

        var authResponse = new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return authResponse;
    }

public async Task Logout(string? refreshToken)
    {

        if (refreshToken == null)
        {
            return;
        }
        var tokenHash = _refreshTokenService.HashToken(refreshToken);

        var storedToken = await _usersContext.RefreshTokens.FirstOrDefaultAsync(x => x.TokenHash == tokenHash);

        if (storedToken == null)
        {
            return;
        }

        storedToken.IsRevoked = true;

        await _usersContext.SaveChangesAsync();

    }

public async Task<AuthResponseDto?> Refresh(string primalRefreshToken)
    {
        
        var tokenHash = _refreshTokenService.HashToken(primalRefreshToken);

        var storedToken = await _usersContext.RefreshTokens.Include(x => x.User).FirstOrDefaultAsync(x => x.TokenHash == tokenHash);

        if (storedToken == null)
        {
            return null;
        }
        if (storedToken.IsRevoked)
        {
            return null;
        }
        if (storedToken.Expires < DateTime.UtcNow)
        {
            return null;
        }

        storedToken.IsRevoked = true;

        var newRefreshEntity = _refreshTokenService.CreateToken(storedToken.UserId, out string refreshToken);

        await _usersContext.RefreshTokens.AddAsync(newRefreshEntity);

        var accessToken = _accessTokenService.CreateAccessToken(storedToken.User);

        await _usersContext.SaveChangesAsync();

        var authResponse = new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return authResponse;

    }


}