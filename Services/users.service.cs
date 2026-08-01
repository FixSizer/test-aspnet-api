using test_ASPNET_api.Models;
using test_ASPNET_api.Data;
using test_ASPNET_api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace test_ASPNET_api.Services;

public class UsersService
{
    
private readonly UsersDbContext _usersContext;
private readonly AccessTokenService _accessTokenService;

private readonly IPasswordService _passwordService;
public UsersService(UsersDbContext usersContext, AccessTokenService accessTokenService, IPasswordService passwordService)
    {
        _usersContext = usersContext;
        _accessTokenService = accessTokenService;
        _passwordService = passwordService;
    }


public async Task<List<UserDataModel>> GetUsers()
    {
        return await _usersContext.Users.ToListAsync();
    }

public async Task<UserDataModel?> GetUser(int id)
    {
        return await _usersContext.Users.FindAsync(id);
    }

public async Task<bool> DeleteUser(int id)
    {
        var user = await _usersContext.Users.FindAsync(id);

        if (user == null)
        {
            return false;
        }
        _usersContext.Users.Remove(user);
        await _usersContext.SaveChangesAsync();
        return true;
    }

public async Task<UserDataModel?> UpdateUserData(int id, UserUpdateDataDto dto)
    {
        var user = await _usersContext.Users.FindAsync(id);
        if (user == null)
        {
            return null;
        }
        if (dto.Name != null )
        {
            user.Name = dto.Name;
        }
        if (dto.Password != null )
        {
            user.PasswordHash = _passwordService.HashPassword(dto.Password);
        }
        if (dto.Email != null)
        {
            user.Email = dto.Email;
        }
        if (dto.PhoneNumber != null)
        {
            user.PhoneNumber = dto.PhoneNumber;
        }

       await _usersContext.SaveChangesAsync();

        return user;

    }
public async Task<UserDataModel?> ChangeUserData(int id, UserDataDto dto)
    {
        var user = await _usersContext.Users.FindAsync(id);

        if (user == null)
        {
            return null;
        }
        
        user.Name = dto.Name;
        user.PasswordHash = _passwordService.HashPassword(dto.Password);
        user.Email = dto.Email;
        user.PhoneNumber = dto.PhoneNumber;
       await _usersContext.SaveChangesAsync();

        return user;
    }


}