using LearningPlatformApi.Services.DataObjects.Response.Module;

namespace LearningPlatformApi.Services.DataObjects.Response.Course;

public record CourseStatisticsDto(
    string CourseId,
    string CourseTitle,
    int TotalEnrollments,
    int ActiveStudents,
    int CompletedStudents,
    double AverageCompletionRate,
    double AverageRating,
    int TotalReviews,
    List<ModuleStatisticsDto> ModulesStatistics);
