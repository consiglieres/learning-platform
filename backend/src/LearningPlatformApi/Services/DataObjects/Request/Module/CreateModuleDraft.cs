using LearningPlatformApi.Domain.Entities.Page;

namespace LearningPlatformApi.Services.DataObjects;

public record CreateModuleDraft(string Name, string CourseId, int ModuleOrder, CoursePage Page);