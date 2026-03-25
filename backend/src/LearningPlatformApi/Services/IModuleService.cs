using OneOf;
using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Services.DataObjects.Request;
using LearningPlatformApi.Services.DataObjects.Response.Shared;

namespace LearningPlatformApi.Services;

public interface IModuleService
{
    Task<OneOf<NotFound, ValidationFailed, Course>> AddModuleAsync(
        string courseId,
        CreateModuleRequest request,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Course>> UpdateModuleAsync(
        string courseId,
        string moduleId,
        UpdateModuleRequest request,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Course>> DeleteModuleAsync(
        string courseId,
        string moduleId,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Course>> ReorderModulesAsync(
        string courseId,
        ReorderModulesRequest request,
        CancellationToken cancellationToken = default);

}