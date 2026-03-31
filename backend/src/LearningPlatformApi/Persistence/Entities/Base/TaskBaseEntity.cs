using System.ComponentModel.DataAnnotations.Schema;
using LearningPlatformApi.Persistence.Entities.Page;

namespace LearningPlatformApi.Persistence.Entities.Base;

[NotMapped]
public abstract class TaskBaseEntity(string id) : VersionableDbEntity<string>(id)
{
    public string Name { get; set; }

    public int Order { get; set; }

    public string DifficultyCategory { get; set; }

    public int DifficultyPoints { get; set; }

    public string LessonId { get; set; }

    public LessonEntity Lesson { get; set; }

    public string? PageId { get; set; }

    public PageEntity? Page { get; set; }
}