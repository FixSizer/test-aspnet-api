using test_ASPNET_api.Models;
using test_ASPNET_api.Data;
using test_ASPNET_api.DTOs;

namespace test_ASPNET_api.Services;

public class UserAuthService
{
    
private readonly AuthDbContext _authContext;

public UserAuthService(AuthDbContext authContext)
    {
        _authContext = authContext;
    }
public List<UserDataModel> GetUsers()
    {
        return _authContext.Users.ToList();
    }

public UserDataModel? GetUser(int id)
    {
        return _authContext.Users.Find(id);
    }

public UserDataModel CreateUser(UserDataDto dto)
    {
        var user = new UserDataModel {
        Name = dto.Name,
        Email = dto.Email,
        PhoneNumber = dto.PhoneNumber,
        Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };
        _authContext.Users.Add(user);
        _authContext.SaveChanges();

        return user;
    }
public bool DeleteUser(int id)
    {
        var user = _authContext.Users.Find(id);

        if (user == null)
        {
            return false;
        }
        _authContext.Users.Remove(user);
        _authContext.SaveChanges();
        return true;
    }

public UserDataModel? UpdateUserData(int id, UserChangeDataDto dto)
    {
        var user = _authContext.Users.Find(id);
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
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        }
        if (dto.Email != null)
        {
            user.Email = dto.Email;
        }
        if (dto.PhoneNumber != null)
        {
            user.PhoneNumber = dto.PhoneNumber;
        }

        _authContext.SaveChanges();
        
        return user;

    }

}