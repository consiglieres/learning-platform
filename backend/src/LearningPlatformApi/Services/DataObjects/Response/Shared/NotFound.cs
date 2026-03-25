namespace LearningPlatformApi.Services.DataObjects.Response.Shared;

public record NotFound(
    string ResourceType,
    string ResourceId);