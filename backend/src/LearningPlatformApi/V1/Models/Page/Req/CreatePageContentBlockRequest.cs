using LearningPlatformApi.Domain.ValueObjects.Page;

namespace LearningPlatformApi.V1.Models.Page.Req;

public class CreatePageContentBlockRequest
{
    public int Order { get; set; }
    public required string Data { get; set; }
    public required ContentBlockType Type { get; set; }
}
