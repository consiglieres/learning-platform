using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.HandleStates;
using LearningPlatformApi.Domain.Repositories;
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
    ICourseCategoriesRepository courseCategoriesRepository,
    IPageRepository pageRepository,
    IUnitOfWork unitOfWork) : ICourseService
{
    public async Task<OneOf<OperationNotSucceeded<Error>, Success<Course>>> CreateCourseDraftAsync(
        CreateCourseDraftRequest request, CancellationToken cancellationToken = default)
    {
        var (title, description, categories, user) = request;
        var course = new Course(title, description, user);

        course.AddCategories(categories);
        course.Modules = [];
        await courseRepository.CreateAsync(course, cancellationToken);
        await pageRepository.CreateAsync(course.IntroductionPage, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        var created = await courseRepository.GetByIdAsync(course.Id, cancellationToken);

        return new Success<Course>(created);
    }

    public async Task<OneOf<EntityNotExists, Success<IReadOnlyCollection<TypedCategory>>>> GetCourseCategoriesAsync(
        CancellationToken cancellationToken = default)
    {
        var categories = await courseCategoriesRepository.GetAllCategoriesAsync(cancellationToken);

        return new Success<IReadOnlyCollection<TypedCategory>>(categories);
    }
    
    public async Task<OneOf<EntityNotExists, Success<Course>>> GetCourseAsync(string courseId, CancellationToken cancellationToken = default)
    {
        var draft = await courseRepository.GetByIdAsync(courseId, cancellationToken);
        return new Success<Course>(draft);
    }

    public async Task<OneOf<NotFound, OperationNotSucceeded<Error>, Success<Course>>> UpdateCourseInfoAsync(
        string courseId, UpdateCourseInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var existingCourse = await courseRepository.GetByIdAsync(courseId, cancellationToken);

        var (title, description, categories) = request;
        if (title != null) existingCourse.Title = title;
        if (description != null) existingCourse.Description = description;
        if (categories != null && categories.Any()) existingCourse.ResetCategories(categories);

        await courseRepository.UpdateAsync(existingCourse, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        var updated = await GetCourseAsync(courseId, cancellationToken);

        if (updated.IsT0)
        {
            return new NotFound();
        }

        return new Success<Course>(updated.AsT1.Value);
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
        var courseResult = await GetCourseAsync(courseId, cancellationToken);

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
        var courseResult = await GetCourseAsync(courseId, cancellationToken);

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
        var courseResult = await GetCourseAsync(courseId, cancellationToken);

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
        var courseResult = await GetCourseAsync(courseId, cancellationToken);

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
        var courseResult = await GetCourseAsync(courseId, cancellationToken);

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
        var courseResult = await GetCourseAsync(courseId, cancellationToken);

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