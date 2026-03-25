namespace LearningPlatformApi.Services.DataObjects.Response;

public record ModuleStatisticsDto(
    string ModuleId,
    string ModuleName,
    double AverageCompletionRate,
    int TotalAttempts);