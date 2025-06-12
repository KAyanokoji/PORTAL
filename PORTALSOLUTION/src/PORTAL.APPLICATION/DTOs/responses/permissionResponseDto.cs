// PORTAL.APPLICATION/DTOs/Responses/PermissionResponseDto.cs
namespace PORTAL.APPLICATION.DTOs.Responses;

public class PermissionResponseDto
{
    public int PermissionId { get; set; }
    public string PermissionName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}