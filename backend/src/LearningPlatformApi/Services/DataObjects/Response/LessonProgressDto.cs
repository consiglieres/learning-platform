namespace LearningPlatformApi.Services.DataObjects.Response;

public record LessonProgressDto(
    string TopicId,
    string TopicName,
    int CurrentPoints,
    int RequiredPoints,
    bool IsPassed,
    string CurrentDifficulty,
    int CompletedTasksCount,
    int TotalTasksCount);