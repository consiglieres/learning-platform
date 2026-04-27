using LearningPlatformApi.Domain.ValueObjects.Page;
using LearningPlatformApi.V1.Models.Account.Res;
using LearningPlatformApi.V1.Models.Base;
using LearningPlatformApi.V2.Account.Res;

namespace LearningPlatformApi.V1.Models.Page.Res;

// todo: добавить методы поиска 
// todo: добавить shortinfo для page и course
public class V1PageDetailResDto : V1PageResDto
{
    public required List<PageVersionInfoDto> VersionHistory { get; set; }
    public required PageDiffDto? ChangesFromPreviousVersion { get; set; }
}

public class PageVersionInfoDto
{
    public required VersionDto Version { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public required V1UserResDto CreatedBy { get; set; }
    public int ContentBlocksCount { get; set; }
    public string? ChangeDescription { get; set; }
}

public class PageDiffDto
{
    public List<BlockDiffDto> AddedBlocks { get; set; } = new();
    public List<BlockDiffDto> RemovedBlocks { get; set; } = new();
    public List<BlockDiffDto> ModifiedBlocks { get; set; } = new();
}

public class BlockDiffDto
{
    public int Order { get; set; }
    public ContentBlockType Type { get; set; }
    public string? OldData { get; set; }
    public string? NewData { get; set; }
}

public class PageComparisonResDto
{
    public required V1PageResDto SourceVersion { get; set; }
    public required V1PageResDto TargetVersion { get; set; }
    public required PageDiffDto Differences { get; set; }
}