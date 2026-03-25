using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Services.DataObjects.Request;
using LearningPlatformApi.Services.DataObjects.Request.Task;
using OneOf;
using LearningPlatformApi.Services.DataObjects.Response.Shared;
using LearningPlatformApi.Services.DataObjects.Response.Task;

namespace LearningPlatformApi.Services;

public interface ITaskService
{
    Task<OneOf<NotFound, TaskDto>> GetTaskForExecutionAsync(
        string courseId,
        string moduleId,
        string topicId,
        string taskId,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Course>> ReorderTasksAsync(
        string courseId,
        string moduleId,
        string topicId,
        GetCourseRequest request,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Course>> DeleteTaskAsync(
        string courseId,
        string moduleId,
        string topicId,
        string taskId,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Course>> UpdateTaskAsync(
        string courseId,
        string moduleId,
        string topicId,
        string taskId,
        UpdateTaskRequest request,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Course>> AddTaskAsync(
        string courseId,
        string moduleId,
        string topicId,
        CreateTaskRequest request,
        CancellationToken cancellationToken = default);
}