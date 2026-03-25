using LearningPlatformApi.Services.DataObjects.Response.Shared;

namespace LearningPlatformApi.Services.DataObjects.Response;

public record CoursePreviewDto(
    string Id,
    string Title,
    string Description,
    string? ThumbnailUrl,
    CourseStatus Status,
    IReadOnlyList<CategoryDto> Categories,
    int ModulesCount,
    int TotalTasksCount,
    AuthorInfo Author,
    DateTimeOffset CreatedAt);