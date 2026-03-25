using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.ValueObjects;

namespace LearningPlatformApi.Domain.Base.Impl;

public abstract record PublicationWorkflowEntity<TKey> : VersionableEntity<TKey>, IPublicationWorkflow
{
    public string? ModerationComment { get; private set; }

    public DateTimeOffset? SubmittedForModerationAt { get; private set; }

    public User? SubmittedBy { get; private set; }

    public DateTimeOffset? PublishedAt { get; private set; }

    public User? PublishedBy { get; private set; }

    public PublicationWorkflowStatus Status
    {
        get;
        private set
        {
            field = value;
            OnStatusChanged();
        }
    }

    protected PublicationWorkflowEntity(TKey id) : base(id)
    {
        Status = PublicationWorkflowStatus.Draft;
    }

    public virtual void SubmitForModeration(User author)
    {
        if (Status != PublicationWorkflowStatus.Draft && Status != PublicationWorkflowStatus.Rejected)
            throw new DomainException($"Cannot submit entity with status {Status} for moderation");

        if (!CanBeSubmitted())
            throw new DomainException("Entity is incomplete and cannot be submitted");

        Status = PublicationWorkflowStatus.PendingModeration;
        SubmittedBy = author;
        SubmittedForModerationAt = DateTimeOffset.UtcNow;
        ModerationComment = null;
        MarkAsUpdated(author, DateTimeOffset.UtcNow);
    }

    public virtual void Approve(User moderator, string? comment = null)
    {
        if (Status != PublicationWorkflowStatus.PendingModeration)
            throw new DomainException($"Cannot approve entity with status {Status}");

        Status = PublicationWorkflowStatus.Published;
        PublishedBy = moderator;
        PublishedAt = DateTimeOffset.UtcNow;
        ModerationComment = comment;
        MarkAsUpdated(moderator, DateTimeOffset.UtcNow);
    }

    public virtual void Reject(User moderator, string reason)
    {
        if (Status != PublicationWorkflowStatus.PendingModeration)
            throw new DomainException($"Cannot reject entity with status {Status}");

        if (string.IsNullOrWhiteSpace(reason))
            throw new DomainException("Rejection reason is required");

        Status = PublicationWorkflowStatus.Rejected;
        ModerationComment = reason;
        MarkAsUpdated(moderator, DateTimeOffset.UtcNow);
    }

    public virtual void Unpublish(User user)
    {
        if (Status != PublicationWorkflowStatus.Published)
            throw new DomainException($"Cannot unpublish entity with status {Status}");

        Status = PublicationWorkflowStatus.Draft;
        PublishedBy = null;
        PublishedAt = null;
        MarkAsUpdated(user, DateTimeOffset.UtcNow);
    }

    public virtual void Archive(User user)
    {
        if (Status == PublicationWorkflowStatus.Archived)
            throw new DomainException("Entity is already archived");

        Status = PublicationWorkflowStatus.Archived;
        MarkAsUpdated(user, DateTimeOffset.UtcNow);
    }

    public virtual void RestoreFromArchive(User user)
    {
        if (Status != PublicationWorkflowStatus.Archived)
            throw new DomainException("Entity is not archived");

        Status = PublicationWorkflowStatus.Draft;
        MarkAsUpdated(user, DateTimeOffset.UtcNow);
    }

    /// <summary>
    /// Проверка, можно ли отправлять на модерацию
    /// </summary>
    public abstract bool CanBeSubmitted();

    /// <summary>
    /// Виртуальный метод, вызываемый при изменении статуса
    /// </summary>
    protected virtual void OnStatusChanged()
    {
        // Можно переопределить для дополнительной логики
    }
}