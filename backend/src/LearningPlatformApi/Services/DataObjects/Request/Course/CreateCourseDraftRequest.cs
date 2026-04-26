using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Entities.Courses;

namespace LearningPlatformApi.Services.DataObjects.Request.Course;

public sealed record CreateCourseDraftRequest(
    string Title,
    string Description,
    IReadOnlyCollection<TypedCategory> Categories,
    User Creator);