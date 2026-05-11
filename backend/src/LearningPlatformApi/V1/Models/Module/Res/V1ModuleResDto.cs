using LearningPlatformApi.V1.Models.Base;
using LearningPlatformApi.V1.Models.Lessons;
using LearningPlatformApi.V1.Models.Lessons.Res;
using LearningPlatformApi.V1.Models.Page;

namespace LearningPlatformApi.V1.Models.Module.Res;

public class V1ModuleResDto : AuditableResDto
{
    public required string Name { get; set; }
    public int ModuleOrder { get; set; }
    public required string CourseId { get; set; }

    public required List<V1LessonShortResDto> Lessons { get; set; }

    public required V1PageResDto Page { get; set; }
}