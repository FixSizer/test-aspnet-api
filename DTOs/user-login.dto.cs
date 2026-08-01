using System.ComponentModel.DataAnnotations;

namespace test_ASPNET_api.DTOs;

public class UserLoginDto
{
    
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;


}