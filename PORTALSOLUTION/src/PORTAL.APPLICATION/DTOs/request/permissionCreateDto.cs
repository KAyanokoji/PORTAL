using System.ComponentModel.DataAnnotations;

namespace PORTAL.APPLICATION.DTOs.request;

public class PermissionCreateDto
{
    [Required(ErrorMessage = "Permission name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public required string PermissionName { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
}