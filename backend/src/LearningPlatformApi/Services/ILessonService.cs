using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Services.DataObjects.Request;
using OneOf;
using LearningPlatformApi.Services.DataObjects.Response.Lesson;
using LearningPlatformApi.Services.DataObjects.Response.Shared;

namespace LearningPlatformApi.Services;

public interface ILessonService
{
    Task<OneOf<NotFound, LessonContentDto>> GetLessonContentAsync(
        string courseId,
        string moduleId,
        string topicId,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Course>> ReorderLessonsAsync(
        string courseId,
        string moduleId,
        ReorderLessonsRequest request,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Course>> DeleteLessonAsync(
        string courseId,
        string moduleId,
        string topicId,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Course>> UpdateLessonAsync(
        string courseId,
        string moduleId,
        string topicId,
        UpdateLessonRequest request,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Course>> AddLessonAsync(
        string courseId,
        string moduleId,
        CreateLessonRequest request,
        CancellationToken cancellationToken = default);
}