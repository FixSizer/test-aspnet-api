using test_ASPNET_api.Models;

public class RefreshToken
{
    
    public int Id { get; set; }

    public string TokenHash { get; set; } = null!;

    public DateTime Expires { get; set; }

    public bool IsRevoked { get; set; }

    public DateTime CreationTime { get; set; }

    public int UserId { get; set; }

    public UserDataModel User { get; set; } = null!;

}