using LearningPlatformApi.Services.DataObjects.Response.Shared;

namespace LearningPlatformApi.Services.DataObjects.Request.Course;

public record GetMyCoursesRequest(int Page = 1, int PageSize = 20, CourseStatusFilter? Status = null);