using LearningPlatformApi.Domain.ValueObjects.Course;

namespace LearningPlatformApi.Domain.Entities.Courses;

public record TypedCategory(CategoryType Type, Category Value);