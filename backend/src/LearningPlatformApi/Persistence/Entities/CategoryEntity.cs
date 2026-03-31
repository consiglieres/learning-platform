
namespace LearningPlatformApi.Persistence.Entities;

public class CategoryEntity
{
    public string TypeCode { get; set; }

    public string ValueCode { get; set; }

    public string TypeName { get; set; }

    public string ValueName { get; set; }
    
    public IReadOnlyCollection<CourseEntity> Courses { get; set; }
}