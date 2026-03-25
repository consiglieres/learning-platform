namespace LearningPlatformApi.Services.DataObjects.Request;

public sealed record CodingTaskData(
    string ProblemDescription,
    string InitialCode,
    string TestCode,
    string? ProgrammingLanguage);