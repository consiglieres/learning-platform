using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.V1.Models.Account.Res;
using LearningPlatformApi.V2.Account.Res;

namespace LearningPlatformApi.V1.Models.Base;

public class PublicationWorkflowResDto : AuditableResDto
{
    public string? ModerationComment { get; set; }

    public DateTimeOffset? SubmittedForModerationAt { get; set; }

    public V1UserResDto? SubmittedBy { get; set; }

    public DateTimeOffset? PublishedAt { get; set; }

    public V1UserResDto? PublishedBy { get; set; }

    public PublicationWorkflowStatus Status { get; set; }
}