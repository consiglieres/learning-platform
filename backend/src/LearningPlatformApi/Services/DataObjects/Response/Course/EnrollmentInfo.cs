namespace LearningPlatformApi.Services.DataObjects.Response.Course;

public record EnrollmentInfo(
    DateTimeOffset EnrolledAt,
    CourseProgressDto Progress);