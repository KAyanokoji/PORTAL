namespace PORTAL.DOMAIN.Entities;

public class Permission : BaseEntity
{
    public int PermissionId { get;  set; }
    public required string PermissionName { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<RolePermission>? RolePermissions { get; set; }
}