namespace LearningPlatformApi.V1.Models.Module.Req;

public class UpdateModuleRequest
{
    public required string Name { get; set; }

    public int ModuleOrder { get; set; }
}