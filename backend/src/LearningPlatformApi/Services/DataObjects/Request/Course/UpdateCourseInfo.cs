using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Entities.Page;

namespace LearningPlatformApi.Services.DataObjects;

public record UpdateCourseInfo(
    string Title,
    string Description,
    List<TypedCategory> Categories,
    Page IntroductionPage);