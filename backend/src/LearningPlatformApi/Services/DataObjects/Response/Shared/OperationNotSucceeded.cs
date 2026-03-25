namespace LearningPlatformApi.Services.DataObjects.Response.Shared;

public record OperationNotSucceeded(
    string Message,
    object? Details = null);