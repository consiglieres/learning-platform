namespace LearningPlatformApi.Services.DataObjects.Response.Module;

public record ModuleProgressDto(
    string ModuleId,
    string ModuleName,
    int TotalTopics,
    int CompletedTopics,
    double CompletionPercentage);