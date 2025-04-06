using PORTAL.APPLICATION.Services;

namespace PORTAL.INFRASTRUCTURE.Services;

public class PasswordHasher:IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
    }

    public bool VerifyPassword(string hashedPassword, string inputPassword)
    {
        return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
    }
}