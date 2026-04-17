using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.ValueObjects.Page;

namespace LearningPlatformApi.Domain.Entities.Page;

public record Page(string Id, int Order, PageType PageType)
    : VersionableEntity<string>(Id)
{

    public int Order { get; set; } = Order;

    public PageType Type { get; set; } = PageType;

    public IReadOnlyCollection<PageContentBlock> ContentBlocks { get; set; } = [];

    public static Page EmptyPage(PageType pageType)
    {
        return new Page(Guid.NewGuid().ToString(), 1, pageType);
    }
}