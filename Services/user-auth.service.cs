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

public string MakeHashPassword(string? password)
    {

        string _password = BCrypt.Net.BCrypt.HashPassword(password);
        return _password;

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
        Password = MakeHashPassword(dto.Password)
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

public UserDataModel? UpdateUserData(int id, UserUpdateDataDto dto)
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
            user.Password = MakeHashPassword(dto.Password);
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
public UserDataModel? ChangeUserData(int id, UserDataDto dto)
    {
        var user = _authContext.Users.Find(id);

        if (user == null)
        {
            return null;
        }
        
        user.Name = dto.Name;
        user.Password = MakeHashPassword(dto.Password);
        user.Email = dto.Email;
        user.PhoneNumber = dto.PhoneNumber;
        _authContext.SaveChanges();

        return user;
    }

}