namespace LearningPlatformApi.Services.DataObjects.Response;

public record LessonStructureDto(
    string Id,
    string Name,
    int Order,
    int PassThreshold,
    int TasksCount,
    bool HasTheory);