namespace LearningPlatformApi.Services.DataObjects.Request.Task;

public sealed record CreateTaskRequest(string Name, string Difficulty, TaskType Type, object TaskData, int? Order = null);