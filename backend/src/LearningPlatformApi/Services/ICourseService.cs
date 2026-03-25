using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.HandleStates;
using LearningPlatformApi.Services.DataObjects.Request;
using LearningPlatformApi.Services.DataObjects.Request.Course;
using LearningPlatformApi.Services.DataObjects.Request.Task;
using LearningPlatformApi.Services.DataObjects.Response;
using LearningPlatformApi.Services.DataObjects.Response.Course;
using LearningPlatformApi.Services.DataObjects.Response.Lesson;
using LearningPlatformApi.Services.DataObjects.Response.Shared;
using LearningPlatformApi.Services.DataObjects.Response.Task;
using OneOf;
using OneOf.Types;
using Error = LearningPlatformApi.Domain.HandleStates.Error;
using NotFound = OneOf.Types.NotFound;
using Success = OneOf.Types.Success;
using ValidationFailed = LearningPlatformApi.Domain.HandleStates.ValidationFailed;

namespace LearningPlatformApi.Services;

public interface ICourseService
{
    Task<OneOf<OperationNotSucceeded<Error>, Success<Course>>> CreateCourseDraftAsync(
        CreateCourseDraftRequest request,
        CancellationToken cancellationToken = default);

    Task<OneOf<EntityNotExists, Success<Course>>> GetCourseDraftAsync(
        string courseId,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, OperationNotSucceeded<Error>, Course>> UpdateCourseInfoAsync(
        string courseId,
        UpdateCourseInfoRequest request,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, Success<Error>>> DeleteCourseDraftAsync(
        string courseId,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Success>> PublishCourseAsync(
        string courseId,
        bool submitForModeration = true,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, Success>> UnpublishCourseAsync(
        string courseId,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, Success>> ArchiveCourseAsync(
        string courseId,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, Success>> RestoreCourseFromArchiveAsync(
        string courseId,
        CancellationToken cancellationToken = default);

    Task<PagedResult<CoursePreviewDto>> GetMyCoursesAsync(
        GetMyCoursesRequest request,
        CancellationToken cancellationToken = default);

    /*Task<PagedResult<CoursePreviewDto>> GetPendingModerationCoursesAsync(
        GetPendingCoursesRequest request,
        CancellationToken cancellationToken = default);*/

    Task<PagedResult<CoursePreviewDto>> SearchCoursesAsync(
        SearchCoursesRequest request,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, CourseProgressDto>> GetCourseProgressAsync(
        string courseId,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, CourseStatisticsDto>> GetCourseStatisticsAsync(
        string courseId,
        CancellationToken cancellationToken = default);

    Task<bool> CanEditCourseAsync(
        string courseId,
        CancellationToken cancellationToken = default);

    Task<bool> CanViewCourseAsync(
        string courseId,
        CancellationToken cancellationToken = default);
}