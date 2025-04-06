namespace PORTAL.DOMAIN.Entities;

public abstract class BaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }
    public string CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }

}