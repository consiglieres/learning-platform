using LearningPlatformApi.Services.DataObjects.Response.Lesson;

namespace LearningPlatformApi.Services.DataObjects.Response.Module;

public record ModuleStructureDto(
    string Id,
    string Name,
    int Order,
    IReadOnlyCollection<LessonStructureDto> Topics);