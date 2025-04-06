namespace PORTAL.DOMAIN.Entities;

public class RolePermission
{
    private RolePermission()
    {
    }

    public RolePermission(Role role, Permission permission)
    {
        Role = role;
        Permission = permission;
    }

    public int RoleId { get; private set; }
    public int PermissionId { get; private set; }
    public Role Role { get; private set; }
    public Permission Permission { get; private set; }
}