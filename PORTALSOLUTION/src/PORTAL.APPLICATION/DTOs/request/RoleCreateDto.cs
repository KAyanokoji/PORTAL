namespace PORTAL.APPLICATION.DTOs.request;

public class RoleCreateDto
{
    public required string RoleName { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}