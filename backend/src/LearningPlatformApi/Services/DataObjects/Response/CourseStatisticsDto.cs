namespace LearningPlatformApi.Services.DataObjects.Response;

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
    