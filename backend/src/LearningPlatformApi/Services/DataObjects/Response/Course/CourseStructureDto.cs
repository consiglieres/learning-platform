using LearningPlatformApi.Services.DataObjects.Response.Module;

namespace LearningPlatformApi.Services.DataObjects.Response.Course;

public record CourseStructureDto(
    string Id,
    string Title,
    IReadOnlyCollection<ModuleStructureDto> Modules);