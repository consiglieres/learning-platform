using LearningPlatformApi.Domain.ValueObjects.Page;
using LearningPlatformApi.V1.Models.Base;

namespace LearningPlatformApi.V1.Models.Page;

public class V1PageResDto : AuditableResDto
{
    public int Order { get; set; }

    public required PageType Type { get; set; }

    public required List<V1PageContentBlock> ContentBlocks { get; set; }
}