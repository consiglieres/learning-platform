using LearningPlatformApi.V1.Models.Base;
using LearningPlatformApi.V1.Models.Page;
using LearningPlatformApi.V1.Models.Tasks;

namespace LearningPlatformApi.V1.Models.Lessons;

public class V1LessonResDto : VersionableResDto
{
    public required string Name { get; set; }

    public int LessonOrder { get; set; }

    public int PassThreshold { get; set; }

    public required V1PageResDto PageContent { get; set; }

    public required string ModuleId { get; set; }

    public required List<V1TaskShortInfo> Tasks { get; set; }
}