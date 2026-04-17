using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.ValueObjects.Page;

namespace LearningPlatformApi.Domain.Entities.Page;

public record Page(string Id, int Order, PageType Type)
    : VersionableEntity<string>(Id)
{

    public int Order { get; set; } = Order;

    public PageType Type { get; set; } = Type;

    public IReadOnlyCollection<PageContentBlock> ContentBlocks { get; set; } = [];

    public static Page EmptyPage(PageType pageType, User creator)
    {
        var page = new Page(Guid.NewGuid().ToString(), 1, pageType);
        page.MarkAsCreated(creator, DateTimeOffset.UtcNow);
        
        return page;
    }
}