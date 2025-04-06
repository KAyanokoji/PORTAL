using PORTAL.DOMAIN.Entities;
using PORTAL.SHARED;

namespace PORTAL.APPLICATION.Services;

public interface IAuthService
{
    Task<string> Authenticate(Login login);
    Task<dynamic> Register(Register register);
}