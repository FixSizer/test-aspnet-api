using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace test_ASPNET_api.Services;


using test_ASPNET_api.Models;

public class AccessTokenService
{
    
private readonly IConfiguration _configuration;

public AccessTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

public string CreateAccessToken(UserDataModel user)
    {
        var claims = new[]
        {
            
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

            new Claim(ClaimTypes.Name, user.Name),

            new Claim(ClaimTypes.Role, user.Role.ToString())

        };
        var expireMinutes = int.Parse(_configuration["Jwt:AccessToken:ExpireMinutes"]!);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:AccessToken:Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var accessToken = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddMinutes(expireMinutes), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(accessToken);
    }
    
}