namespace LearningPlatformApi.Services.DataObjects.Response.Shared;

public record PagedResult<T>(
    IEnumerable<T> Items,
    int TotalCount,
    int Page,
    int PageSize,
    int TotalPages);