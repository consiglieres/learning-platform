using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.ValueObjects.Page;

namespace LearningPlatformApi.Domain.Entities.Page;

public record PageContentBlock(string Id, string PageId, int Order, ContentBlockType Type, string Data)
    : AuditableEntity<string>(Id)
{
    public string PageId { get; } = PageId;

    public int Order { get; } = Order;

    public string Data { get; } = Data;

    public ContentBlockType Type { get; } = Type;
}