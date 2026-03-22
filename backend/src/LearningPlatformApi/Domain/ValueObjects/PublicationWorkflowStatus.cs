namespace LearningPlatformApi.Domain.ValueObjects;

public enum PublicationWorkflowStatus : byte
{
    Draft,
    PendingModeration,
    Published,
    Rejected,
    Archived
}