using LearningPlatformApi.Persistence.Entities.Base;
using LearningPlatformApi.Persistence.Entities.Page;

namespace LearningPlatformApi.Persistence.Entities;

public class ModuleEntity(string id) : AuditableDbEntity<string>(id)
{
    public string Name { get; set; }

    public int ModuleOrder { get; set; }

    public PageEntity Page { get; set; }

    public string PageId { get; set; }

    public string CourseId { get; set; }
    
    public IReadOnlyCollection<LessonEntity> Lessons { get; set; }
}