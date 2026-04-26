using LearningPlatformApi.V1.Models.Base;
using LearningPlatformApi.V2.Account.Res;

namespace LearningPlatformApi.V1.Models.Module.Res;

public class ModuleVersionInfoDto
{
    public required VersionDto Version { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public required V1UserResDto CreatedBy { get; set; }
    public int LessonsCount { get; set; }
    public string ChangeDescription { get; set; } = string.Empty;
}