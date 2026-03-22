using LearningPlatformApi.Domain.Base;
using LearningPlatformApi.Domain.Entities;

namespace LearningPlatformApi.Persistence.Entities.Base;

public abstract class AuditableDbEntity<TId>(TId id) : DbEntity<TId>(id), ICreatable, IUpdatable, ISoftDeletable
{
    public DateTimeOffset CreatedAt { get; private set; }

    public string CreatedBy { get; private set; } = null!;

    public DateTimeOffset? UpdatedAt { get; private set; }

    public string? UpdatedBy { get; private set; }

    public DateTimeOffset? DeletedAt { get; private set; }

    public string? DeletedBy { get; private set; }

    public void MarkAsCreated(User cratedBy, DateTimeOffset createdAt)
    {
        CreatedAt = createdAt;
        CreatedBy = cratedBy.UserName;
        UpdatedAt = createdAt;
        UpdatedBy = cratedBy.UserName;
    }

    public void MarkAsUpdated(User updatedBy, DateTimeOffset updatedAt)
    {
        UpdatedAt = updatedAt;
        UpdatedBy = updatedBy.UserName;
    }

    public void MarkAsDeleted(User deletedBy, DateTimeOffset deletedAt)
    {
        DeletedAt = deletedAt;
        DeletedBy = deletedBy.UserName;
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