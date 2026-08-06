using System.Security.Cryptography;
using System.Text;

public class RefreshTokenService
{
    
    private readonly IConfiguration _configuration;

    public RefreshTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken()
    {
        byte[] bytes = RandomNumberGenerator.GetBytes(64);

        return Convert.ToBase64String(bytes);
    }

    public string HashToken(string token)
    {
        using var sha256 = SHA256.Create();

        byte[] bytes = Encoding.UTF8.GetBytes(token);

        byte[] hash = sha256.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }

    public RefreshToken CreateToken(int userId, out string refreshToken)
    {
        refreshToken = GenerateToken();
        return new RefreshToken
        {
            TokenHash = HashToken(refreshToken),
            UserId = userId,
            CreationTime = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(int.Parse(_configuration["Jwt:RefreshToken:ExpireDays"]!)),
            IsRevoked = false
        };
    }

}