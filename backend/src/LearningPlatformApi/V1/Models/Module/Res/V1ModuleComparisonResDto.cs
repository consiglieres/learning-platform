namespace LearningPlatformApi.V1.Models.Module.Res;

public class ModuleComparisonResDto
{
    public V1ModuleResDto SourceVersion { get; set; } = null!;
    public V1ModuleResDto TargetVersion { get; set; } = null!;
    public ModuleDiffDto Differences { get; set; } = new();
}