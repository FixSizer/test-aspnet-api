namespace test_ASPNET_api.Services;

public class PasswordService : IPasswordService {

private static readonly int SALT_ROUNDS = 10;

public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, SALT_ROUNDS);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

}