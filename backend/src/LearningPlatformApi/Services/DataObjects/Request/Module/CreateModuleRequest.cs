using LearningPlatformApi.Domain.Entities.Page;

namespace LearningPlatformApi.Services.DataObjects.Request;

public sealed record CreateModuleRequest(string Name, int? Order = null, Page? CoursePage = null);
