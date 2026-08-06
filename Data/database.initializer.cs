using Microsoft.EntityFrameworkCore;
using test_ASPNET_api.Models;

namespace test_ASPNET_api.Data;

public class DbInitializer
{
    private readonly UsersDbContext _usersDbContext;

    private readonly IConfiguration _configuration;

    private readonly IPasswordService _passwordService;

    public DbInitializer(UsersDbContext usersDbcontext, IConfiguration configuration, IPasswordService passwordService)
    {
        _usersDbContext = usersDbcontext;
        _configuration = configuration;
        _passwordService = passwordService;
    }
    public async Task Initialize()
    {
        if (!await _usersDbContext.Users.AnyAsync(x => x.Role == UserRole.ADMIN))
        {
            var admin = new UserDataModel
            {
              Name = _configuration["Admin:Username"]!,
              PasswordHash = _passwordService.HashPassword(_configuration["Admin:Password"]!),
              Role = UserRole.ADMIN,
              Email = _configuration["Admin:Email"]!,
              RegistrationDate = DateTime.UtcNow
            };
            await _usersDbContext.Users.AddAsync(admin);
            await _usersDbContext.SaveChangesAsync();

        }
    }
}