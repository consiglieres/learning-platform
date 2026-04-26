namespace LearningPlatformApi.Services.DataObjects.Response.Module;

public record ModuleStatisticsDto(
    string ModuleId,
    string ModuleName,
    double AverageCompletionRate,
    int TotalAttempts);