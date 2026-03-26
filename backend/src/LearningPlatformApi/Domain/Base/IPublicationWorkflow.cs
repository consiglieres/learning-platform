using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.ValueObjects;

namespace LearningPlatformApi.Domain.Base;

public interface IPublicationWorkflow
{
    PublicationWorkflowStatus Status { get; }
    string? ModerationComment { get; }
    DateTimeOffset? SubmittedForModerationAt { get; }
    User? SubmittedBy { get; }
    DateTimeOffset? PublishedAt { get; }
    User? PublishedBy { get; }

    void SubmitForModeration(User author);
    void Approve(User moderator, string? comment = null);
    void Reject(User moderator, string reason);
    void Unpublish(User user);
    void Archive(User user);
    void RestoreFromArchive(User user);
    bool CanBeSubmitted();
}