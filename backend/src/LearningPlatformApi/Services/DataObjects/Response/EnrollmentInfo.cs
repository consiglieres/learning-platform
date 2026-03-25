namespace LearningPlatformApi.Services.DataObjects.Response;

public record EnrollmentInfo(
    DateTimeOffset EnrolledAt,
    CourseProgressDto Progress);