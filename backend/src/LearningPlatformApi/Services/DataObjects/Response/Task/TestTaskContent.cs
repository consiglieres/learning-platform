namespace LearningPlatformApi.Services.DataObjects.Response.Task;

public record TestTaskContent(string Question, IReadOnlyCollection<string> Options);