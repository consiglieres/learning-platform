namespace LearningPlatformApi.Services.DataObjects.Request;

public sealed record UpdateTaskRequest(
    string? Name,
    string? Difficulty,
    object? TaskData,
    int? Order);