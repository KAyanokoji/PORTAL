namespace PORTAL.APPLICATION.DTOs.request;

public class RoleUpdateDto
{
    public int RoleId { get; set; }
    public required string RoleName { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}