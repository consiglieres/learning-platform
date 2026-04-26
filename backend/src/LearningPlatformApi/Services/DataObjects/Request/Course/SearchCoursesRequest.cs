using LearningPlatformApi.Domain.Entities.Courses;

namespace LearningPlatformApi.Services.DataObjects.Request;

public record SearchCoursesRequest(string? Query, List<TypedCategory>? Categories, int Page = 1, int PageSize = 20);