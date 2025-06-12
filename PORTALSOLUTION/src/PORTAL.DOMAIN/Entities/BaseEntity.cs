namespace PORTAL.DOMAIN.Entities;

public abstract class BaseEntity: ISoftDeletable
{
    public int Id { get;  set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsDeleted { get; set; } = false; // Default to false
}