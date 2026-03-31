using LearningPlatformApi.Persistence.Entities.Base;

namespace LearningPlatformApi.Persistence.Entities.Page;

public class PageEntity(string id) : VersionableDbEntity<string>(id)
{
    public int Order { get; set; }

    public string TypeCode { get; set; }

    public string TypeName { get; set; }

    public IReadOnlyCollection<ContentBlockEntity> ContentBlocks { get; set; }
}