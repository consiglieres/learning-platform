namespace LearningPlatformApi.Persistence.Entities.Base;

public class TaskBaseEntity(string id) : AuditableDbEntity<string>(id)
{
    public string Name { get; set; }
    
    public int Order { get; set; }
    
    public string DifficultyCategory { get; set; }
    
    public int DifficultyPoints { get; set; }
    
    public string LessonId { get; set; }
    
    public LessonEntity Lesson { get; set; }
}