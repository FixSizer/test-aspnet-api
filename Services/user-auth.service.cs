using test_ASPNET_api.Models;
using test_ASPNET_api.Data;
using test_ASPNET_api.DTOs;

namespace test_ASPNET_api.Services;

public class UserAuthService
{
    
private readonly AuthDbContext _context;

public UserAuthService(AuthDbContext context)
    {
        _context = context;
    }
public List<UserDataModel> GetUsers()
    {
        return _context.Users.ToList();
    }

public UserDataModel? GetUser(int id)
    {
        return _context.Users.Find(id);
    }

public UserDataModel CreateUser(CreateUserDto dto)
    {
        var user = new UserDataModel {
        Name = dto.Name,
        Email = dto.Email,
        PhoneNumber = dto.PhoneNumber,
        Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };
        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }

}