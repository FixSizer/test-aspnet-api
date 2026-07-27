namespace test_ASPNET_api.Models;
public class UserDataModel
{
    public int Id { get; set; }

    public string? Name { get; set; }
    
    public string? Email { get; set; }
    
    public string? Password { get; set; }

    public string? PhoneNumber { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

}