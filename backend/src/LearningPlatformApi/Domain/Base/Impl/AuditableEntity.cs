using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Exceptions;

namespace LearningPlatformApi.Domain.Base.Impl;

public abstract record AuditableEntity<TKey>(TKey Id)
    : DomainEntity<TKey>(Id), IAuditable, ICreatable, IUpdatable, ISoftDeletable
{
    public DateTimeOffset CreatedAt { get; set; }
    public User CreatedBy { get; set; } = null!;
    public DateTimeOffset? UpdatedAt { get; set; }
    public User? UpdatedBy { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
    public User? DeletedBy { get; set; }

    public void MarkAsCreated(User createdBy, DateTimeOffset createdAt)
    {
        if (CreatedBy != null)
            throw new DomainException("Entity has already been created");

        CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
        CreatedAt = createdAt;
    }

    public void MarkAsDeleted(User deletedBy, DateTimeOffset deletedAt)
    {
        if (IsDeleted())
            throw new DomainException("Entity is already deleted");

        DeletedBy = deletedBy ?? throw new ArgumentNullException(nameof(deletedBy));
        DeletedAt = deletedAt;
    }

    public void Restore(User deletedBy, DateTimeOffset deletedAt)
    {
        if (!IsDeleted())
            throw new DomainException("Entity is not deleted");

        DeletedBy = null;
        DeletedAt = null;
        MarkAsUpdated(deletedBy, deletedAt);
    }

    public bool IsDeleted()
    {
        return DeletedAt != null || DeletedBy != null;
    }

    public void MarkAsUpdated(User updatedBy, DateTimeOffset updatedAt)
    {
        if (IsDeleted())
            throw new DomainException("Cannot update a deleted entity");

        UpdatedBy = updatedBy ?? throw new ArgumentNullException(nameof(updatedBy));
        UpdatedAt = updatedAt;
    }
}