namespace LearningPlatformApi.Services.DataObjects.Request;

public sealed record CreateLessonRequest(string Name, int LessonOrder, int PassThreshold);