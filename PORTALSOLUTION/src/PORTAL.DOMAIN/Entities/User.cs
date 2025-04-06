namespace PORTAL.DOMAIN.Entities;

public class User : BaseEntity
{
   

    public int UserId { get; set; }
    public required string  Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsLockedOut { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public string? TimeZone { get; set; }
    public string? Culture { get; set; }
    public string? AvatarUrl { get; set; }
    public string? SecurityStamp { get; set; }
    public string? TwoFactorSecret { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTime? PasswordExpirationDate { get; set; }
    public int FailedLoginAttempts { get; set; }

    public DateTime? LockoutEndDate { get; set; }

    // Navigation properties
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>(); // Many-to-many relationship with Role

    
}