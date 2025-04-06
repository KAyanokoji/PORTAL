using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PORTAL.DOMAIN.Entities;
using PORTAL.DOMAIN.Exceptions;
using PORTAL.DOMAIN.Interfaces;
using PORTAL.INFRASTRUCTURE.Persistence;


namespace PORTAL.INFRASTRUCTURE.Repositories;

public class AuthRepo(ApplicationDbContext context, ILogger<User> logger) : IAuthRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<User> _logger = logger;
    private static readonly string[] value = new[] { "Username cannot be empty." };

    public Task<User> Authenticate(string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserByUserName(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ModelValidationException(new Dictionary<string, string[]>
            {
                { nameof(username), value }
            });

        try { 
         var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == username);

            return user;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user {Username}", username);
            throw;
        }
    }

    public async Task<User> Register(User register)
    {
        try
        {
            var reg = await _context.Users.AddAsync(register);
            await _context.SaveChangesAsync(); // Save to database

            return reg.Entity;
        }
        catch (Exception)
        {
            throw;
        }
    }

}