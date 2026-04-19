using LearningPlatformApi.V1.Models.Base;

namespace LearningPlatformApi.V1.Models.Module;

public class V1ModuleShortResDto : AuditableResDto
{
    public required string Name { get; set; }
    public int ModuleOrder { get; set; }
    public required string CourseId { get; set; }
    
    public required string[] LessonIds { get; set; }
    
    public required string IntroductionPageId { get; set; }
}