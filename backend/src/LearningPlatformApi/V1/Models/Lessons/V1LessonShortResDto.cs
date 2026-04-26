using LearningPlatformApi.V1.Models.Base;

namespace LearningPlatformApi.V1.Models.Lessons;

public class V1LessonShortResDto : AuditableResDto
{
    public required string Name { get; set; }

    public int LessonOrder { get; set; }

    public int PassThreshold { get; set; }
    
    public required string ModuleId { get; set; }
}