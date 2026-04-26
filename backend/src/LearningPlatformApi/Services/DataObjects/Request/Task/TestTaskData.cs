namespace LearningPlatformApi.Services.DataObjects.Request;

public sealed record TestTaskData(
    string Question,
    List<string> Options,
    string CorrectAnswer);