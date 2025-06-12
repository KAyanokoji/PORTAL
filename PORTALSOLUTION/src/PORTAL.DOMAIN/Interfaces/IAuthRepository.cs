using PORTAL.DOMAIN.Entities;
using PORTAL.SHARED;

namespace PORTAL.DOMAIN.Interfaces;

public interface IAuthRepository
{
    Task<User> Authenticate(string username, string password);
    Task<User> Register(User register);
    Task<User> GetUserByUserName(string username);
    Task<User> GetUSerByEmail(string email);
}