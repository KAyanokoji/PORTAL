namespace PORTAL.DOMAIN.Entities;

public class Role : BaseEntity
{
    public Role(string roleName, string description)
    {
        RoleName = roleName;
        Description = description;
    }

    public int RoleId { get; private set; }
    public string RoleName { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; } = true;
    public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
    public ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();
}