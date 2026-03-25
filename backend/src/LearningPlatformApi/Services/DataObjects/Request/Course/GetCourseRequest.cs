namespace LearningPlatformApi.Services.DataObjects.Request;

public record GetCourseRequest(int Page = 1, int PageSize = 20, string? SortBy = "CreatedAt", bool Descending = true);