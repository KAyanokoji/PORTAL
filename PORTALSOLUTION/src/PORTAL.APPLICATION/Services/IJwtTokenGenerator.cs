using PORTAL.DOMAIN.Entities;


namespace PORTAL.APPLICATION.Services
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(User user);
    }
}
