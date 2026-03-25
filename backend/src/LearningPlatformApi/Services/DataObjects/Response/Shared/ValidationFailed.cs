namespace LearningPlatformApi.Services.DataObjects.Response.Shared;

public record ValidationFailed(
    List<string> Errors);