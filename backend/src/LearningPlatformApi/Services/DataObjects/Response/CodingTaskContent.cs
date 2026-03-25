namespace LearningPlatformApi.Services.DataObjects.Response;

public record CodingTaskContent(
    string ProblemDescription,
    string InitialCode,
    string? ProgrammingLanguage);