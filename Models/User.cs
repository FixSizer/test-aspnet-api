using System.ComponentModel.DataAnnotations;

namespace FirstProject.Models;
public class User
{
    public int Id { get; set; }

    [MinLength(3)]
    [MaxLength(25)]
    [Required]
    public string name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string email { get; set; } = string.Empty;
    [MinLength(8)]
    [MaxLength(25)]
    [Required]
    public string password { get; set; } = string.Empty;

    public string phone { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
}