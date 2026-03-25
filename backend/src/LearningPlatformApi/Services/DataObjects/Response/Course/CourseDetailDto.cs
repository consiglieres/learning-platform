using LearningPlatformApi.Services.DataObjects.Response.Module;
using LearningPlatformApi.Services.DataObjects.Response.Shared;

namespace LearningPlatformApi.Services.DataObjects.Response.Course;

public record CourseDetailDto(
    string Id,
    string Title,
    string Description,
    CourseStatus Status,
    IReadOnlyCollection<CategoryDto> Categories,
    IReadOnlyCollection<ModulePreviewDto> Modules,
    TimeSpan EstimatedDuration,
    DateTimeOffset CreatedAt,
    DateTimeOffset? PublishedAt,
    AuthorInfo Author,
    EnrollmentInfo? Enrollment);