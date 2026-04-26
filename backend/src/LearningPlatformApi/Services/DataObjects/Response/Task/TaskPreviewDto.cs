using LearningPlatformApi.Services.DataObjects.Response.Shared;

namespace LearningPlatformApi.Services.DataObjects.Response.Task;

public record TaskPreviewDto(
    string Id,
    string Name,
    string Difficulty,
    TaskType Type,
    int Points,
    bool IsCompleted);