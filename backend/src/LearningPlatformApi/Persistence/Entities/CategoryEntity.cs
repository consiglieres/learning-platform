
namespace LearningPlatformApi.Persistence.Entities;

public class CategoryEntity
{
    public string TypeName { get; set; }

    public string ValueName { get; set; }
    
    public IReadOnlyCollection<CourseEntity> Courses { get; set; }
}