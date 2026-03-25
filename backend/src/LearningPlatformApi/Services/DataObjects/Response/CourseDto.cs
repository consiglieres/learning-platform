using LearningPlatformApi.Services.DataObjects.Response.Shared;

namespace LearningPlatformApi.Services.DataObjects.Response;

public sealed record CourseDto(
    string Id,
    string Title,
    string Description,
    CourseStatus Status,
    IReadOnlyCollection<CategoryDto> Categories,
    int ModulesCount,
    int TotalTasksCount,
    TimeSpan EstimatedDuration,
    DateTimeOffset CreatedAt,
    AuthorInfo Author);