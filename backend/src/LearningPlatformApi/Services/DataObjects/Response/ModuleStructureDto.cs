namespace LearningPlatformApi.Services.DataObjects.Response;

public record ModuleStructureDto(
    string Id, 
    string Name,
    int Order, 
    IReadOnlyCollection<LessonStructureDto> Topics);