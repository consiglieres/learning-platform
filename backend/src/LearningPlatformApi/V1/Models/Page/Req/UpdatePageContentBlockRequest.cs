using LearningPlatformApi.Domain.ValueObjects.Page;

namespace LearningPlatformApi.V1.Models.Page.Req;

public class UpdatePageContentBlockRequest
{
    public string? Id { get; set; } // Для существующих блоков
    public int Order { get; set; }
    public required string Data { get; set; }
    public required ContentBlockType Type { get; set; }
}