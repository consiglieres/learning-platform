namespace LearningPlatformApi.V1.Models.Module.Req;

public class CopyModuleRequest
{
    public string SourceModuleId { get; set; } = string.Empty;
    public int? SourceVersionOrder { get; set; }
    public string TargetCourseId { get; set; } = string.Empty;
    public int NewModuleOrder { get; set; }
    public string? NewModuleName { get; set; }
}