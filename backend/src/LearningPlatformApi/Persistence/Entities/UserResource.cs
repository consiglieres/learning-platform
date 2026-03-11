namespace LearningPlatformApi.Persistence.Entities;

public class UserResource
{
    public string UserId { get; set; }

    public Guid ResourceId { get; set; }

    public string Permission { get; set; } // Read, Write, Delete, Manage

    public DateTime GrantedAt { get; set; } = DateTime.UtcNow;

    public string GrantedBy { get; set; } = string.Empty;

    public virtual AppUser User { get; set; } = null!;

    public virtual Resource Resource { get; set; } = null!;
}