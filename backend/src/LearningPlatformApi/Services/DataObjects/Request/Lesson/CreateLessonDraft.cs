using LearningPlatformApi.Domain.Entities.Page;

namespace LearningPlatformApi.Services.DataObjects;

public record CreateLessonDraft(string Name, int LessonOrder, string ModuleId, CoursePage Page);