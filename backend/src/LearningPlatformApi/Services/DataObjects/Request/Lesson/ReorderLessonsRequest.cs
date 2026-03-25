namespace LearningPlatformApi.Services.DataObjects.Request;

public sealed record ReorderLessonsRequest(List<string> LessonIdsInOrder);