using LearningPlatformApi.Domain.ValueObjects.Page;

namespace LearningPlatformApi.Persistence.Entities.Page;

public class ContentBlockEntity
{
    public string Id { get; set; }
    
    public int Order { get; set; }
    
    public string Data { get; set; }
    
    public ContentBlockType Type { get; set; }
}