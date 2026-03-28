using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.ValueObjects.Page;

namespace LearningPlatformApi.Domain.Entities.Page;

public record Page(string Id, PageType Type, IReadOnlyCollection<PageContentBlock> ContentBlocks)
    : VersionableEntity<string>(Id)
{
    public int Order { get; set; }

    private PageType Type { get; set; } = Type;

    public IReadOnlyCollection<PageContentBlock> ContentBlocks { get; set; } = ContentBlocks;

    public static Page EmptyPage(PageType pageType)
        => new (Guid.NewGuid().ToString(), pageType, []);
}