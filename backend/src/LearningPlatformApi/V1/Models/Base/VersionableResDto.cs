namespace LearningPlatformApi.V1.Models.Base;

public class VersionableResDto : AuditableResDto
{
    public required VersionDto Version { get; init; }
}