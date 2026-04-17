using LearningPlatformApi.Domain.Entities.Courses;

namespace LearningPlatformApi.Services.DataObjects.Request.Course;

public sealed record UpdateCourseInfoRequest(
    string? Title,
    string? Description,
    IReadOnlyCollection<TypedCategory>? Categories);