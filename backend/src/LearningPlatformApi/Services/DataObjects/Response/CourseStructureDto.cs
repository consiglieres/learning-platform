namespace LearningPlatformApi.Services.DataObjects.Response;

public record CourseStructureDto(
    string Id,
    string Title,
    IReadOnlyCollection<ModuleStructureDto> Modules);