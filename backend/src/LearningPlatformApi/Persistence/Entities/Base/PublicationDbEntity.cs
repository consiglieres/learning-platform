using LearningPlatformApi.Domain.ValueObjects;

namespace LearningPlatformApi.Persistence.Entities.Base;

public abstract class PublicationDbEntity<TId>(TId id) : VersionableDbEntity<TId>(id)
{
    public string? ModerationComment { get; private set; }

    public DateTimeOffset? SubmittedForModerationAt { get; private set; }

    public string? SubmittedBy { get; private set; }

    public DateTimeOffset? PublishedAt { get; private set; }

    public string? PublishedBy { get; private set; }

    public PublicationWorkflowStatus Status { get; private set; }
}