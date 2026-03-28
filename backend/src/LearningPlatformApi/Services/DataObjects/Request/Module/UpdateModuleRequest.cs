using LearningPlatformApi.Domain.Entities.Page;

namespace LearningPlatformApi.Services.DataObjects.Request;

public sealed record UpdateModuleRequest(string? Name, int? Order, Page? CoursePage = null);