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

namespace LearningPlatformApi.Services.Impl;

public class CourseService() : ICourseService
{
    public Task<OneOf<OperationNotSucceeded<Error>, Success<Course>>> CreateCourseDraftAsync(CreateCourseDraftRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<OneOf<EntityNotExists, Success<Course>>> GetCourseDraftAsync(string courseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<OneOf<NotFound, OperationNotSucceeded<Error>, Course>> UpdateCourseInfoAsync(string courseId, UpdateCourseInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<OneOf<NotFound, Success<Error>>> DeleteCourseDraftAsync(string courseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<OneOf<NotFound, ValidationFailed, Success>> PublishCourseAsync(string courseId, bool submitForModeration = true,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<OneOf<NotFound, Success>> UnpublishCourseAsync(string courseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<OneOf<NotFound, Success>> ArchiveCourseAsync(string courseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<OneOf<NotFound, Success>> RestoreCourseFromArchiveAsync(string courseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResult<CoursePreviewDto>> GetMyCoursesAsync(GetMyCoursesRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResult<CoursePreviewDto>> SearchCoursesAsync(SearchCoursesRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<OneOf<NotFound, CourseProgressDto>> GetCourseProgressAsync(string courseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<OneOf<NotFound, CourseStatisticsDto>> GetCourseStatisticsAsync(string courseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CanEditCourseAsync(string courseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CanViewCourseAsync(string courseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}