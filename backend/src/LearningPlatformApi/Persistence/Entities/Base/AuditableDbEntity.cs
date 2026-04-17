using LearningPlatformApi.Domain.Base;
using LearningPlatformApi.Domain.Entities;

namespace LearningPlatformApi.Persistence.Entities.Base;

public abstract class AuditableDbEntity<TId>(TId id) : DbEntity<TId>(id), ICreatable, IUpdatable, ISoftDeletable
{
    public DateTimeOffset CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;
    
    public UserEntity CreatedByUser { get; set; } = null!;

    public DateTimeOffset? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
    
    public UserEntity? UpdatedByUser { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }
    
    public UserEntity? DeletedByUser { get; set; }

    public void MarkAsCreated(User cratedBy, DateTimeOffset createdAt)
    {
        CreatedAt = createdAt;
        CreatedBy = cratedBy.Id;
        UpdatedAt = createdAt;
        UpdatedBy = cratedBy.Id;
    }

    public void MarkAsUpdated(User updatedBy, DateTimeOffset updatedAt)
    {
        UpdatedAt = updatedAt;
        UpdatedBy = updatedBy.Id;
    }

    public void MarkAsDeleted(User deletedBy, DateTimeOffset deletedAt)
    {
        DeletedAt = deletedAt;
        DeletedBy = deletedBy.Id;
    }

    public void Restore()
    {
        DeletedAt = null;
        DeletedBy = null;
    }

    public bool IsDeleted()
    {
        return DeletedAt != null || DeletedBy != null;
    }
}