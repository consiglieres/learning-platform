using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.HandleStates;
using LearningPlatformApi.Services.DataObjects.Request;
using LearningPlatformApi.Services.DataObjects.Request.Course;
using LearningPlatformApi.Services.DataObjects.Response.Course;
using LearningPlatformApi.Services.DataObjects.Response.Shared;
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

    Task<OneOf<EntityNotExists, Success<Course>>> GetCourseLastAsync(
        string courseId,
        CancellationToken cancellationToken = default);

    Task<OneOf<EntityNotExists, Success<Course>>> GetCourseVersionAsync(
        string courseId,
        int version,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, OperationNotSucceeded<Error>, Success<Course>>> UpdateCourseInfoAsync(
        string courseId,
        UpdateCourseInfoRequest request,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, Success>> DeleteCourseAsync(
        string courseId, User user,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Success>> SubmitForModerationCourseAsync(
        string courseId, User user, CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Success>> ApprovePublishCourseAsync(
        string courseId, User user, ModerationCourseComment? comment,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Success>> RejectCourseAsync(
        string courseId, User user, ModerationCourseComment comment,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Success>> UnpublishCourseAsync(
        string courseId, User user, CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Success>> ArchiveCourseAsync(
        string courseId, User user, CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Success>> RestoreCourseFromArchiveAsync(
        string courseId, User user,
        CancellationToken cancellationToken = default);

    Task<PagedResult<CoursePreviewDto>> GetMyCoursesAsync(
        GetMyCoursesRequest request,
        CancellationToken cancellationToken = default);

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