using System.ComponentModel.DataAnnotations;

namespace test_ASPNET_api.DTOs;

public class CreateUserDto
{
    
    [MinLength(3)]
    [MaxLength(25)]
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [MinLength(8)]
    [MaxLength(25)]
    [Required]
    public string Password { get; set; } = string.Empty;

    [RegularExpression(@"^\+[1-9]\d{7,14}$", ErrorMessage = "Enter the number in international format, for example, +7992......")]
    public string? PhoneNumber { get; set; }


}