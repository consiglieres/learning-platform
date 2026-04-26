namespace LearningPlatformApi.V1.Models.Module.Res;

public class V1ModuleSettingsDto
{
    public bool IsPublished { get; set; }
    public bool IsRequired { get; set; }
    public int MinScoreToPass { get; set; }
    public int TimeLimitInMinutes { get; set; }
    public Dictionary<string, string> CustomProperties { get; set; } = new();
}