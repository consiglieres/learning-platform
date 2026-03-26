using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.ValueObjects.Page;

namespace LearningPlatformApi.Domain.Entities.Page;

public record CoursePage(string Id, PageType Type, IReadOnlyCollection<PageContentBlock> ContentBlocks)
    : DomainEntity<string>(Id)
{
    public int Order { get; set; }

    private PageType Type { get; set; } = Type;

    public IReadOnlyCollection<PageContentBlock> ContentBlocks { get; set; } = ContentBlocks;

    public static CoursePage EmptyPage(PageType pageType)
        => new CoursePage(Guid.NewGuid().ToString(), pageType, []);
}