using LearningPlatformApi.Persistence.Entities.Base;
using LearningPlatformApi.Persistence.Entities.Page;

namespace LearningPlatformApi.Persistence.Entities;

public class LessonEntity(string id) : VersionableDbEntity<string>(id)
{
    public string Name { get; set; }
    
    public int LessonOrder { get; set; }
    
    public int PassThreshold { get; set; }
    
    public string ModuleId { get; set; }
    
    public ModuleEntity Module { get; set; }
    
    public PageEntity PageEntity { get; set; }
    
    public string PageId { get; set; }

    public IReadOnlyCollection<TaskBaseEntity> Tasks { get; set; }
}