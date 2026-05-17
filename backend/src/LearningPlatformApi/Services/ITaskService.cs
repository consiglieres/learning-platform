using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Services.DataObjects.Request;
using LearningPlatformApi.Services.DataObjects.Request.Task;
using LearningPlatformApi.Services.DataObjects.Response.Shared;
using LearningPlatformApi.V1.Models.Tasks;
using LearningPlatformApi.V1.Models.Tasks.Req;
using OneOf;
using OneOf.Types;
using NotFound = LearningPlatformApi.Services.DataObjects.Response.Shared.NotFound;
using Success = LearningPlatformApi.Services.DataObjects.Response.Shared.Success;

namespace LearningPlatformApi.Services;

public interface ITaskService
{
    Task<OneOf<NotFound, IReadOnlyCollection<V1TaskShortInfo>>> GetTasksAsync(string lessonId, CancellationToken cancellationToken = default);
    
    Task<OneOf<NotFound, IReadOnlyCollection<V1CodingTaskResDto>>> GetCodingTasksAsync(string lessonId, CancellationToken cancellationToken = default);
    
    Task<OneOf<NotFound, IReadOnlyCollection<V1TestTaskResDto>>> GetTestTasksAsync(string lessonId, CancellationToken cancellationToken = default);
    
    Task<OneOf<NotFound, V1TestTaskResDto>> GetTestTaskForExecutionAsync(string taskId,
        CancellationToken cancellationToken = default);
    
    Task<OneOf<NotFound, V1CodingTaskResDto>> GetCodingTaskForExecutionAsync(string taskId,
        CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, Success>> ReorderTasksAsync(string lessonId, User user, 
        V1ReorderLessonTasksRequestDto request, CancellationToken cancellationToken = default);

    Task<OneOf<ValidationFailed, Success>> DeleteTaskAsync(string taskId, User user,
        CancellationToken cancellationToken = default);

    Task<OneOf<V1TestTaskResDto>> UpdateTestTaskAsync(string taskId, V1UpdateTestTaskReqDto request, 
        User user, CancellationToken cancellationToken = default);
    
    Task<OneOf<V1CodingTaskResDto>> UpdateCodingTaskAsync(string taskId, V1UpdateCodingTaskReqDto request, 
        User user, CancellationToken cancellationToken = default);

    Task<OneOf<NotFound, ValidationFailed, V1CodingTaskResDto>> AddCodingTaskAsync(string lessonId, User user, V1CreateCodingTaskReqDto request, 
        CancellationToken cancellationToken = default);
    
    Task<OneOf<NotFound, ValidationFailed, V1TestTaskResDto>> AddTestTaskAsync(string lessonId, User user,
        V1CreateTestTaskReqDto request, CancellationToken cancellationToken = default);
}