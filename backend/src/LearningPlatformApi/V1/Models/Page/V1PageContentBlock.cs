using LearningPlatformApi.Domain.ValueObjects.Page;
using LearningPlatformApi.V1.Models.Base;

namespace LearningPlatformApi.V1.Models.Page;

public class V1PageContentBlock : AuditableResDto
{
    public required string PageId { get; set; }

    public int Order { get; set; }

    public required string Data { get; set; }

    public required ContentBlockType Type { get; set; }
}