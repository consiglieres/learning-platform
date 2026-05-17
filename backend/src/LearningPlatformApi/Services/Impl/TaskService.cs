using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.Entities.Tasks;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.Repositories;
using LearningPlatformApi.Domain.ValueObjects.Page;
using LearningPlatformApi.Domain.ValueObjects.Task;
using LearningPlatformApi.Persistence.Repositories.Base;
using LearningPlatformApi.Services.DataObjects.Response.Shared;
using LearningPlatformApi.V1.Mapper;
using LearningPlatformApi.V1.Models.Tasks;
using LearningPlatformApi.V1.Models.Tasks.Req;
using OneOf;
using NotFound = LearningPlatformApi.Services.DataObjects.Response.Shared.NotFound;
using Success = LearningPlatformApi.Services.DataObjects.Response.Shared.Success;

namespace LearningPlatformApi.Services.Impl;

public class TaskService(ILessonRepository lessonRepository, ITestTaskRepository testTaskRepository, 
    ICodingTaskRepository codingTaskRepository, IV1ResDtoMapper resDtoMapper, IUnitOfWork unitOfWork)
    : ITaskService
{
    public async Task<OneOf<NotFound, IReadOnlyCollection<V1TaskShortInfo>>> GetTasksAsync(string lessonId, 
        CancellationToken cancellationToken = default)
    {
        var lesson = await lessonRepository.GetByIdAsync(lessonId, cancellationToken);

        var tasks = lesson.CodingTasks.Select(x => x as BaseTask)
            .Concat(lesson.TestTasks.Select(x => x as BaseTask))
            .Select(resDtoMapper.MapShort).ToList();

        return tasks;
    }

    public async Task<OneOf<NotFound, IReadOnlyCollection<V1CodingTaskResDto>>> GetCodingTasksAsync(
        string lessonId, CancellationToken cancellationToken = default)
    {
        var lesson = await lessonRepository.GetByIdAsync(lessonId, cancellationToken);

        var tasks = lesson.CodingTasks.Select(resDtoMapper.Map).ToList();

        return tasks;
    }

    public async Task<OneOf<NotFound, IReadOnlyCollection<V1TestTaskResDto>>> GetTestTasksAsync(string lessonId, 
        CancellationToken cancellationToken = default)
    {
        var lesson = await lessonRepository.GetByIdAsync(lessonId, cancellationToken);

        var tasks = lesson.TestTasks.Select(resDtoMapper.Map).ToList();

        return tasks;
    }

    public async Task<OneOf<NotFound, V1TestTaskResDto>> GetTestTaskForExecutionAsync(string taskId, CancellationToken cancellationToken = default)
    {
        var testTask = await testTaskRepository.GetByIdAsync(taskId, cancellationToken);
        return resDtoMapper.Map(testTask);
    }

    public async Task<OneOf<NotFound, V1CodingTaskResDto>> GetCodingTaskForExecutionAsync(string taskId, CancellationToken cancellationToken = default)
    {
        var testTask = await codingTaskRepository.GetByIdAsync(taskId, cancellationToken);
        return resDtoMapper.Map(testTask);
    }

    public async Task<OneOf<NotFound, ValidationFailed, Success>> ReorderTasksAsync(string lessonId, User user,
        V1ReorderLessonTasksRequestDto request, CancellationToken cancellationToken = default)
    {
        var lesson = await lessonRepository.GetByIdAsync(lessonId, cancellationToken);
        
        var codingTaskByIds = lesson.CodingTasks.ToDictionary(x => x.Id);
        var testTaskByIds = lesson.TestTasks.ToDictionary(x => x.Id);

        if (!request.TasksOrderIds.All(x => codingTaskByIds.ContainsKey(x) || testTaskByIds.ContainsKey(x)))
        {
            throw new DomainException("Not all tasks founded");
        }

        var i = 1;
        foreach (var taskId in request.TasksOrderIds)
        {
            if (codingTaskByIds.TryGetValue(taskId, out var codingTask))
            {
                codingTask.Order = i;
                i++;
            }
            else if (testTaskByIds.TryGetValue(taskId, out var testTask))
            {
                testTask.Order = i;
                i++;
            }
        }

        foreach (var codingTask in codingTaskByIds.Values)
        {
            codingTask.MarkAsUpdated(user, DateTimeOffset.UtcNow);
            await codingTaskRepository.UpdateAsync(codingTask, cancellationToken);
        }
        foreach (var testTask in testTaskByIds.Values)
        {
            testTask.MarkAsUpdated(user, DateTimeOffset.UtcNow);
            await testTaskRepository.UpdateAsync(testTask, cancellationToken);
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new Success("Reordered successfully");
    }

    public async Task<OneOf<ValidationFailed, Success>> DeleteTaskAsync(string taskId, User user, CancellationToken cancellationToken = default)
    {
        var testTask = await testTaskRepository.FindByIdAsync(taskId, cancellationToken);
        var codingTask = await codingTaskRepository.FindByIdAsync(taskId, cancellationToken);

        if (codingTask == null && testTask == null)
        {
            throw new DomainException("Task entity not found");
        }

        if (codingTask != null)
        {
            await codingTaskRepository.DeleteAsync(codingTask.Id, user, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return new Success("Deleted successfully");
        }
        if (testTask != null)
        {
            await codingTaskRepository.DeleteAsync(testTask.Id, user, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return new Success("Deleted successfully");
        }

        throw new Exception("Task not found");
    }

    public async Task<OneOf<V1TestTaskResDto>> UpdateTestTaskAsync(string taskId, V1UpdateTestTaskReqDto request, 
        User user, CancellationToken cancellationToken = default)
    {
        var testTask = await testTaskRepository.GetByIdAsync(taskId, cancellationToken);

        testTask.Name = request.Name ?? testTask.Name;
        testTask.Order = request.Order ?? testTask.Order;
        var diffName = request.DifficultyName ?? testTask.Difficulty.Name;
        var diffPoints = request.DifficultyPoints ?? testTask.Difficulty.BasePoints;
        testTask.Difficulty = new Difficulty(diffName, diffPoints);
        testTask.Question = request.Question ?? testTask.Question;
        testTask.Options = request.Options ?? testTask.Options;
        testTask.Answer = request.Answers ?? testTask.Answer;
        testTask.MarkAsUpdated(user, DateTimeOffset.UtcNow);
        
        await testTaskRepository.UpdateAsync(testTask, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        var update = await testTaskRepository.GetByIdAsync(taskId, cancellationToken);
        
        return resDtoMapper.Map(update);
    }

    public async Task<OneOf<V1CodingTaskResDto>> UpdateCodingTaskAsync(string taskId, V1UpdateCodingTaskReqDto request, 
        User user, CancellationToken cancellationToken = default)
    {
        var codingTask = await codingTaskRepository.GetByIdAsync(taskId, cancellationToken);

        codingTask.Name = request.Name ?? codingTask.Name;
        codingTask.Order = request.Order ?? codingTask.Order;
        var diffName = request.DifficultyName ?? codingTask.Difficulty.Name;
        var diffPoints = request.DifficultyPoints ?? codingTask.Difficulty.BasePoints;
        codingTask.Difficulty = new Difficulty(diffName, diffPoints);
        codingTask.InitialCode = request.InitialCode ?? codingTask.InitialCode;
        codingTask.TestCode = request.TestCode ?? codingTask.TestCode;
        codingTask.MarkAsUpdated(user, DateTimeOffset.UtcNow);
        
        await codingTaskRepository.UpdateAsync(codingTask, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        var update = await codingTaskRepository.GetByIdAsync(taskId, cancellationToken);
        
        return resDtoMapper.Map(update);
    }

    public async Task<OneOf<NotFound, ValidationFailed, V1CodingTaskResDto>> AddCodingTaskAsync(string lessonId,
        User user, V1CreateCodingTaskReqDto request, CancellationToken cancellationToken = default)
    {
        var codingTask = new CodingTask(request.Name, request.Order, new Difficulty(request.DifficultyName, request.DifficultyPoints),
            lessonId, Page.EmptyPage(PageType.Task, user), request.InitialCode, request.TestCode, user);
        
        await codingTaskRepository.CreateAsync(codingTask, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        var created = await codingTaskRepository.GetByIdAsync(codingTask.Id, cancellationToken);
        return resDtoMapper.Map(created);
    }

    public async Task<OneOf<NotFound, ValidationFailed, V1TestTaskResDto>> AddTestTaskAsync(string lessonId, 
        User user, V1CreateTestTaskReqDto request, CancellationToken cancellationToken = default)
    {
        var testTask = new TestTask(request.Name, request.Order, new Difficulty(request.DifficultyName, 
                request.DifficultyPoints), lessonId, Page.EmptyPage(PageType.Task, user), request.Question, 
            request.Options, request.Answers, user);
        
        await testTaskRepository.CreateAsync(testTask, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        var created = await testTaskRepository.GetByIdAsync(testTask.Id, cancellationToken);
        return resDtoMapper.Map(created);
    }
}