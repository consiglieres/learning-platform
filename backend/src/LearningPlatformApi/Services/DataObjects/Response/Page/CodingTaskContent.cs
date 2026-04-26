namespace LearningPlatformApi.Services.DataObjects.Response.Page;

public record CodingTaskContent(
    string ProblemDescription,
    string InitialCode,
    string? ProgrammingLanguage);