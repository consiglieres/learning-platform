using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Exceptions;

namespace LearningPlatformApi.Domain.Base.Impl;

public abstract record AuditableEntity<TKey>(TKey Id)
    : DomainEntity<TKey>(Id), IAuditable, ICreatable, IUpdatable, ISoftDeletable
{
    public DateTimeOffset CreatedAt { get; private set; }
    public User CreatedBy { get; private set; } = null!;
    public DateTimeOffset? UpdatedAt { get; private set; }
    public User? UpdatedBy { get; private set; }

    public DateTimeOffset? DeletedAt { get; private set; }
    public User? DeletedBy { get; private set; }
    public bool IsDeleted => DeletedAt.HasValue;

    public void MarkAsCreated(User createdBy, DateTimeOffset createdAt)
    {
        if (CreatedBy != null)
            throw new DomainException("Entity has already been created");

        CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
        CreatedAt = createdAt;
    }

    public void MarkAsUpdated(User updatedBy, DateTimeOffset updatedAt)
    {
        if (IsDeleted)
            throw new DomainException("Cannot update a deleted entity");

        UpdatedBy = updatedBy ?? throw new ArgumentNullException(nameof(updatedBy));
        UpdatedAt = updatedAt;
    }

    public void MarkAsDeleted(User deletedBy, DateTimeOffset deletedAt)
    {
        if (IsDeleted)
            throw new DomainException("Entity is already deleted");

        DeletedBy = deletedBy ?? throw new ArgumentNullException(nameof(deletedBy));
        DeletedAt = deletedAt;
    }

    public void Restore()
    {
        if (!IsDeleted)
            throw new DomainException("Entity is not deleted");

        DeletedBy = null;
        DeletedAt = null;
    }
}