using LearningPlatformApi.Domain.ValueObjects.Page;
using LearningPlatformApi.Persistence.Entities.Base;

namespace LearningPlatformApi.Persistence.Entities.Page;

public class ContentBlockEntity(int id) : AuditableDbEntity<int>(id)
{
    public string PageId { get; set; }

    public int Order { get; set; }

    public string Data { get; set; }

    public ContentBlockType Type { get; set; }
}