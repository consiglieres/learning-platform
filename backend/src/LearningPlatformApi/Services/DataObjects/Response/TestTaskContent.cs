namespace LearningPlatformApi.Services.DataObjects.Response;

public record TestTaskContent(string Question, IReadOnlyCollection<string> Options);