using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.ValueObjects.Page;

namespace LearningPlatformApi.Domain.Entities.Page;

public record Page(string Id)
    : VersionableEntity<string>(Id)
{

    public int Order { get; set; }

    public PageType Type { get; set; }

    public IReadOnlyCollection<PageContentBlock> ContentBlocks { get; set; }

    public static Page EmptyPage(PageType pageType)
    {
        return new Page(Guid.NewGuid().ToString())
        {
            Order = int.MaxValue,
            Type = pageType,
            ContentBlocks = []
        };
    }
}