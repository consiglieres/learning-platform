namespace LearningPlatformApi.Services.DataObjects.Response;

public record ModuleProgressDto(
    string ModuleId,
    string ModuleName,
    int TotalTopics,
    int CompletedTopics,
    double CompletionPercentage);