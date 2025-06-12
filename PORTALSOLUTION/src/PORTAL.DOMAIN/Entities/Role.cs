namespace PORTAL.DOMAIN.Entities;

public class Role : BaseEntity
{
    public int RoleId { get;  set; }
    public required string RoleName { get;  set; }
    public string? Description { get;  set; }
    public bool IsActive { get;  set; } = true;
    public ICollection<UserRole> UserRoles { get;  set; } = new List<UserRole>();
    public ICollection<RolePermission> RolePermissions { get;  set; } = new List<RolePermission>();
}