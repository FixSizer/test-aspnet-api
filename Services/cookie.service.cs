namespace test_ASPNET_api.Services;

public class CookieService
{
    private readonly IConfiguration _configuration;
    public CookieService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void SetRefreshTokenCookie(HttpResponse response, string refreshToken)
    {
        response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(int.Parse(_configuration["Jwt:RefreshToken:ExpireDays"]!))
        }
        );
    }
    public string? GetRefreshToken(HttpRequest request)
    {
        return request.Cookies["refreshToken"];
    }
    public void RemoveRefreshTokenCookie(HttpResponse response)
    {
        response.Cookies.Delete("refreshToken");
    }

}