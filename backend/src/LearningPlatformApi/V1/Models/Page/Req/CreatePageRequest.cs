using LearningPlatformApi.Domain.ValueObjects.Page;

namespace LearningPlatformApi.V1.Models.Page.Req;

public class CreatePageRequest
{
    public int Order { get; set; }
    public required PageType Type { get; set; }
    public required List<CreatePageContentBlockRequest> ContentBlocks { get; set; }
}