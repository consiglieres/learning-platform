namespace LearningPlatformApi.Services.DataObjects.Request;

public record UpdateLessonRequest(string? Name, int? PassThreshold, int? Order);