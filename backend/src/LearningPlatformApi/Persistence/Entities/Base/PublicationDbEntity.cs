using LearningPlatformApi.Domain.ValueObjects;

namespace LearningPlatformApi.Persistence.Entities.Base;

public abstract class PublicationDbEntity<TId>(TId id) : VersionableDbEntity<TId>(id)
{
    public string? ModerationComment { get; set; }

    public DateTimeOffset? SubmittedForModerationAt { get; set; }

    public string? SubmittedBy { get; set; }
    
    public UserEntity? SubmittedByUser { get; set; }

    public DateTimeOffset? PublishedAt { get; set; }

    public string? PublishedBy { get; set; }
    
    public UserEntity? PublishedByUser { get; set; }

    public PublicationWorkflowStatus Status { get; set; }
}