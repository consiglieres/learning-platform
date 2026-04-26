namespace LearningPlatformApi.Services.DataObjects.Response.Lesson;

public record LessonStructureDto(
    string Id,
    string Name,
    int Order,
    int PassThreshold,
    int TasksCount,
    bool HasTheory);