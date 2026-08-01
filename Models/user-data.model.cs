namespace test_ASPNET_api.Models;

public class UserDataModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string PasswordHash { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public UserRole Role { get; set; } = UserRole.USER;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

}