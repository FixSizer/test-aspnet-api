using System.Text.Json.Serialization;

namespace test_ASPNET_api.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRole
{
    USER,
    ADMIN
}
public class UserDataModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public UserRole Role { get; set; } = UserRole.USER;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

}