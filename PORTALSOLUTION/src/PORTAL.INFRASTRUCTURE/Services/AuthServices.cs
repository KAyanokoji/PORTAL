using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using PORTAL.APPLICATION.Services;
using PORTAL.DOMAIN.Entities;
using PORTAL.DOMAIN.Exceptions;
using PORTAL.DOMAIN.Interfaces;
using PORTAL.SHARED;
using Serilog;

namespace PORTAL.INFRASTRUCTURE.Services;

public class AuthServices(IAuthRepository authRepository, IMapper mapper,IPasswordHasher passwordHasher, ILogger<User> logger, IJwtTokenGenerator jwt) : IAuthService
{
    private readonly IAuthRepository _authRepository = authRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly ILogger<User> _logger = logger;
    private readonly IJwtTokenGenerator jwt = jwt;

    public async Task<string> Authenticate(Login request)
    {
        var existUser = await _authRepository.GetUSerByEmail(request.Username) ?? throw new AuthenticationFailedException();
        var checkPassword = _passwordHasher.VerifyPassword(existUser.Password, request.Password);
        if (!checkPassword)
        {
            throw new AuthenticationFailedException();
        }
        string token = jwt.GenerateToken(existUser);
        return token;
        
    }

    public async Task<dynamic> Register(Register register)
    {

            var existUser = await _authRepository.GetUserByUserName(register.Username);
            if (existUser is not null)
            {
                throw new UsernameExistsException(register.Username);
            }
            register.Password = _passwordHasher.HashPassword(register.Password);
            var users = _mapper.Map<User>(register);
            users.CreatedBy = "Saroj";
            var reg = await _authRepository.Register(users);
            return reg;
    }
} 