namespace PORTAL.DOMAIN.Entities;

public class Permission : BaseEntity
{
    public Permission(string permissionName, string description)
    {
        PermissionName = permissionName;
        Description = description;
    }

    public int PermissionId { get; private set; }
    public string PermissionName { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; } = true;
    public ICollection<RolePermission> RolePermissions { get; private set; }
}