using LearningPlatformApi.V1.Models.Base;
using LearningPlatformApi.V1.Models.Courses.Req;

namespace LearningPlatformApi.V1.Models.Courses.Res;

public class V1CourseShort : AuditableResDto
{
    public required string Title { get; set; }

    public required string Description { get; set; }

    public List<V1CourseCategory> Categories { get; set; } = [];
}