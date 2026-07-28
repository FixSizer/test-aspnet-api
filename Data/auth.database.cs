using Microsoft.EntityFrameworkCore;

using test_ASPNET_api.Models;

namespace test_ASPNET_api.Data;

public class AuthDbContext : DbContext
{
    
public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
        
    }


protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDataModel>().Property(u => u.Role).HasConversion<string>();
    }
    
    public DbSet<UserDataModel> Users { get; set; } = null!;

}