using LearningPlatformApi.Domain.Entities.Courses;

namespace LearningPlatformApi.Services.DataObjects.Request;

public sealed record CreateCourseDraftRequest(string Title, string Description, IReadOnlyCollection<TypedCategory> Categories);