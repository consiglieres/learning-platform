namespace LearningPlatformApi.V1.Models.Module.Req;

public class CreateModuleRequest
{
    public required string Name { get; set; }

    public int ModuleOrder { get; set; }

    public required string CourseId { get; set; }
}