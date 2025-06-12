namespace PORTAL.APPLICATION.DTOs.Responses;

public class RoleResponseDto
{
    public int RoleId { get; set; }
    public required string RoleName { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
