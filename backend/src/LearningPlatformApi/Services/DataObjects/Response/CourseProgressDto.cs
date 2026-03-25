namespace LearningPlatformApi.Services.DataObjects.Response;

public class CourseProgressDto(
    string CourseId,
    string CourseTitle,
    int TotalTopics,
    int CompletedTopics,
    int TotalTasks,
    int CompletedTasks,
    int TotalPoints,
    int EarnedPoints,
    double CompletionPercentage,
    List<ModuleProgressDto> ModulesProgress);