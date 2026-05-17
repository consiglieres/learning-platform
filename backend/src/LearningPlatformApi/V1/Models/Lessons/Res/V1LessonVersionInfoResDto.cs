using LearningPlatformApi.V1.Models.Account.Res;
using LearningPlatformApi.V1.Models.Base;

namespace LearningPlatformApi.V1.Models.Lessons.Res;

public class V1LessonVersionInfoResDto
{
    public required VersionDto Version { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public required V1UserResDto CreatedBy { get; set; }
    public int LessonsCount { get; set; }
    public string ChangeDescription { get; set; } = string.Empty;
}