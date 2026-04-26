using LearningPlatformApi.Domain.ValueObjects.Page;
using LearningPlatformApi.Persistence.Entities.Base;

namespace LearningPlatformApi.Persistence.Entities.Page;

public class ContentBlockEntity(string id) : AuditableDbEntity<string>(id)
{
    public string PageId { get; set; }

    public int PageVersion { get; set; }

    public int Order { get; set; }

    public string Data { get; set; }

    public ContentBlockType Type { get; set; }
}