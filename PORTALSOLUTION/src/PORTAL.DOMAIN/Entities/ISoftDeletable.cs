namespace PORTAL.DOMAIN.Entities;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; } 
}