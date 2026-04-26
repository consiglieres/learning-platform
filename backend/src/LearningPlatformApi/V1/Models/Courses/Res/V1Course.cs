using LearningPlatformApi.V1.Models.Base;
using LearningPlatformApi.V1.Models.Courses.Req;
using LearningPlatformApi.V1.Models.Module;
using LearningPlatformApi.V1.Models.Page;

namespace LearningPlatformApi.V1.Models.Courses.Res;

public class V1Course : PublicationWorkflowResDto
{
    public required string Title { get; set; }

    public required string Description { get; set; }

    public List<V1CourseCategory> Categories { get; set; } = [];

    public List<V1ModuleShortResDto> Modules { get; set; } = [];

    public required V1PageResDto IntroductionPage { get; set; }
}