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

public AuthService(UsersDbContext usersContext, AccessTokenService accessTokenService, IPasswordService passwordService)
    {
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
            PhoneNumber = dto.PhoneNumber
        };

        await _usersContext.Users.AddAsync(user);
        await _usersContext.SaveChangesAsync();

        return true;
    }

public async Task<string?> Login(UserLoginDto dto)
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

        return _accessTokenService.CreateAccessToken(user);
    }
}