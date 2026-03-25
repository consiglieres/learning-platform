using LearningPlatformApi.Domain.ValueObjects.Course;

namespace LearningPlatformApi.Services.DataObjects.Request;

public sealed record UpdateCourseInfoRequest(string? Title, string? Description, List<CategoryType> Categories);
