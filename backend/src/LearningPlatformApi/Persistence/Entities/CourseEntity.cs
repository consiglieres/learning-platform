using LearningPlatformApi.Persistence.Entities.Base;
using LearningPlatformApi.Persistence.Entities.Page;

namespace LearningPlatformApi.Persistence.Entities;

public class CourseEntity(string id) : PublicationDbEntity<string>(id)
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public IReadOnlyCollection<CategoryEntity> Categories { get; set; }
    
    public IReadOnlyCollection<ModuleEntity> Modules { get; set; }
    
    public PageEntity IntroductionPage { get; set; }
}