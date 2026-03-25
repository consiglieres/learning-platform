using LearningPlatformApi.Services.DataObjects.Response.Shared;

namespace LearningPlatformApi.Services.DataObjects.Response;

public record TaskDto(
    string Id,
    string Name,
    string Difficulty,
    TaskType Type,
    object Content,
    int Points,
    int? Order);