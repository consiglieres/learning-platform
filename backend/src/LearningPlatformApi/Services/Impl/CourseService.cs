using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.HandleStates;
using LearningPlatformApi.Domain.Repositories;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Persistence.Repositories.Base;
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

public class CourseService(
    ICourseRepository courseRepository,
    IUnitOfWork unitOfWork) : ICourseService
{
    public async Task<OneOf<OperationNotSucceeded<Error>, Success<Course>>> CreateCourseDraftAsync(
        CreateCourseDraftRequest request, CancellationToken cancellationToken = default)
    {
        var (title, description, categories, user) = request;
        var course = new Course(title, description, user);

        course.AddCategories(categories);
        await courseRepository.CreateAsync(course, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        var created = await courseRepository.GetLastAsync(course.Id, cancellationToken);

        return new Success<Course>(created);
    }

    public async Task<OneOf<EntityNotExists, Success<Course>>> GetCourseLastAsync(string courseId,
        CancellationToken cancellationToken = default)
    {
        var draft = await courseRepository.GetLastAsync(courseId, cancellationToken);
        return new Success<Course>(draft);
    }

    public async Task<OneOf<EntityNotExists, Success<Course>>> GetCourseVersionAsync(string courseId, int version,
        CancellationToken cancellationToken = default)
    {
        var draft = await courseRepository.GetAsync(courseId, new EntityVersion(version), cancellationToken);
        return new Success<Course>(draft);
    }

    public async Task<OneOf<NotFound, OperationNotSucceeded<Error>, Success<Course>>> UpdateCourseInfoAsync(
        string courseId, UpdateCourseInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var existingCourse = await courseRepository.GetLastAsync(courseId, cancellationToken);

        var (title, description, categories) = request;
        if (title != null) existingCourse.Title = title;
        if (description != null) existingCourse.Description = description;
        if (categories != null && categories.Any()) existingCourse.ResetCategories(categories);
        var updated = await courseRepository.UpdateAsync(existingCourse, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new Success<Course>(updated);
    }

    public async Task<OneOf<NotFound, Success>> DeleteCourseAsync(string courseId, User user,
        CancellationToken cancellationToken = default)
    {
        await courseRepository.DeleteAsync(courseId, user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new Success();
    }

    public async Task<OneOf<NotFound, ValidationFailed, Success>> ApprovePublishCourseAsync(string courseId, User user,
        ModerationCourseComment? comment, CancellationToken cancellationToken = default)
    {
        var courseResult = await GetCourseLastAsync(courseId, cancellationToken);

        if (courseResult.IsT0)
            return new NotFound();

        var course = courseResult.AsT1.Value; // Success case
        try
        {
            course.Approve(user, comment?.Comment);
            await courseRepository.UpdateAsync(course, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return new ValidationFailed(ex.Message);
        }

        return new Success();
    }

    public async Task<OneOf<NotFound, ValidationFailed, Success>> SubmitForModerationCourseAsync(
        string courseId, User user, CancellationToken cancellationToken = default)
    {
        var courseResult = await GetCourseLastAsync(courseId, cancellationToken);

        if (courseResult.IsT0)
            return new NotFound();

        var course = courseResult.AsT1.Value; // Success case
        try
        {
            course.SubmitForModeration(user);
            await courseRepository.UpdateAsync(course, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return new ValidationFailed(ex.Message);
        }

        return new Success();
    }

    public async Task<OneOf<NotFound, ValidationFailed, Success>> RejectCourseAsync(
        string courseId, User user, ModerationCourseComment comment, CancellationToken cancellationToken = default)
    {
        var courseResult = await GetCourseLastAsync(courseId, cancellationToken);

        if (courseResult.IsT0)
            return new NotFound();

        var course = courseResult.AsT1.Value; // Success case
        try
        {
            if (comment.Comment == null) return new ValidationFailed("Should send reject moderation comment");
            course.Reject(user, comment.Comment);
            await courseRepository.UpdateAsync(course, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return new ValidationFailed(ex.Message);
        }

        return new Success();
    }

    public async Task<OneOf<NotFound, ValidationFailed, Success>> UnpublishCourseAsync(string courseId, User user,
        CancellationToken cancellationToken = default)
    {
        var courseResult = await GetCourseLastAsync(courseId, cancellationToken);

        if (courseResult.IsT0)
            return new NotFound();

        var course = courseResult.AsT1.Value; // Success case
        try
        {
            course.Unpublish(user);
            await courseRepository.UpdateAsync(course, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return new ValidationFailed(ex.Message);
        }

        return new Success();
    }

    public async Task<OneOf<NotFound, ValidationFailed, Success>> ArchiveCourseAsync(string courseId, User user,
        CancellationToken cancellationToken = default)
    {
        var courseResult = await GetCourseLastAsync(courseId, cancellationToken);

        if (courseResult.IsT0)
            return new NotFound();

        var course = courseResult.AsT1.Value; // Success case
        try
        {
            course.Archive(user);
            await courseRepository.UpdateAsync(course, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return new ValidationFailed(ex.Message);
        }

        return new Success();
    }

    public async Task<OneOf<NotFound, ValidationFailed, Success>> RestoreCourseFromArchiveAsync(string courseId,
        User user,
        CancellationToken cancellationToken = default)
    {
        var courseResult = await GetCourseLastAsync(courseId, cancellationToken);

        if (courseResult.IsT0)
            return new NotFound();

        var course = courseResult.AsT1.Value; // Success case
        try
        {
            course.RestoreFromArchive(user);
            await courseRepository.UpdateAsync(course, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return new ValidationFailed(ex.Message);
        }

        return new Success();
    }

    public Task<PagedResult<CoursePreviewDto>> GetMyCoursesAsync(GetMyCoursesRequest request,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResult<CoursePreviewDto>> SearchCoursesAsync(SearchCoursesRequest request,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<OneOf<NotFound, CourseProgressDto>> GetCourseProgressAsync(string courseId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<OneOf<NotFound, CourseStatisticsDto>> GetCourseStatisticsAsync(string courseId,
        CancellationToken cancellationToken = default)
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