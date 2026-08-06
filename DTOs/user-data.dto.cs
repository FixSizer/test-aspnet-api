using System.ComponentModel.DataAnnotations;

namespace test_ASPNET_api.DTOs;

public class UserDataDto
{
    
    [MinLength(3)]
    [MaxLength(25)]
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [MinLength(8)]
    [MaxLength(25)]
    [Required]
    public string Password { get; set; } = null!;

    [RegularExpression(@"^\+[1-9]\d{7,14}$", ErrorMessage = "Enter the number in international format, for example, +7992......")]
    public string? PhoneNumber { get; set; }


}