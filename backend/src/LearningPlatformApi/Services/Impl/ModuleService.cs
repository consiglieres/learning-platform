using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.Repositories;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Repositories.Base;
using LearningPlatformApi.V1.Mapper;
using LearningPlatformApi.V1.Models.Module.Req;
using LearningPlatformApi.V1.Models.Module.Res;

namespace LearningPlatformApi.Services.Impl;

public class ModuleService(IModulesRepository moduleRepository, IV1ResDtoMapper resDtoMapper, IUnitOfWork unitOfWork) : IModuleService
{
    public async Task<V1ModuleResDto> CreateAsync(CreateModuleRequest request, User user, CancellationToken cancellationToken)
    {
        var module = new Module(request.Name, request.ModuleOrder,
            request.CourseId, user, []);

        await moduleRepository.CreateAsync(module, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var createdModule = await moduleRepository.GetByIdAsync(module.Id, cancellationToken);
        return resDtoMapper.Map(createdModule);
    }

    public async Task<V1ModuleResDto> UpdateAsync(string id, User user, UpdateModuleRequest request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var currentModule = await moduleRepository.GetByIdAsync(id, cancellationToken);

            if (currentModule == null)
                throw new DomainException($"Module with id {id} not found");

            var newModule = new Module(currentModule.Id, request.Name,
                request.ModuleOrder, currentModule.CourseId, user, currentModule.Lessons);

            await moduleRepository.UpdateAsync(newModule, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            var createdModule = await moduleRepository.GetByIdAsync(newModule.Id, cancellationToken);
            return resDtoMapper.Map(createdModule);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task<V1ModuleResDto> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var module = await moduleRepository.GetByIdAsync(id, cancellationToken);
        return resDtoMapper.Map(module);
    }

    public async Task DeleteAsync(string id, User user, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            await moduleRepository.DeleteAsync(id, user, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public Task<List<V1ModuleResDto>> GetModulesByIdsAsync(List<string> ids, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<V1ModuleResDto> RestoreAsync(string id, User user, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var deletedModule = await moduleRepository.GetByIdAsync(id, cancellationToken);

            if (deletedModule == null)
                throw new DomainException($"No deleted version found for module {id}");

            deletedModule.Restore(user, DateTimeOffset.UtcNow);

            await moduleRepository.CreateAsync(deletedModule, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return resDtoMapper.Map(deletedModule);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task<IReadOnlyCollection<V1ModuleResDto>> GetModulesByIdsAsync(IReadOnlyCollection<string> ids, CancellationToken cancellationToken)
    {
        var modules = await moduleRepository.GetByIdsAsync(ids, cancellationToken);
        return modules.Select(resDtoMapper.Map).ToList();
    }

    public Task<V1ModuleResDto> ReorderLessonsAsync(string id, List<string> lessonIds, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        /*await unitOfWork.BeginTransactionAsync(cancellationToken);
        
        try
        {
            var currentModule = await moduleRepository.GetLastAsync(id, cancellationToken);
            
            if (currentModule.Lessons.Count != lessonIds.Count)
                throw new DomainException("Number of lesson IDs doesn't match number of lessons");
            
            var lessonsById = currentModule.Lessons.ToDictionary(l => l.Id, l => l);
            var reorderedLessons = new List<ModuleLesson>();
            
            for (var i = 0; i < lessonIds.Count; i++)
            {
                if (!lessonsById.TryGetValue(lessonIds[i], out var lesson))
                    throw new DomainException($"Lesson with id {lessonIds[i]} not found");
                
                var updatedLesson = lesson with { Order = i + 1 };
                reorderedLessons.Add(updatedLesson);
            }
            
            var newModule = new Module(
                id,
                currentModule.CourseId,
                currentModule.Title,
                currentModule.Description,
                currentModule.Order,
                currentModule.Type)
            {
                Lessons = reorderedLessons.OrderBy(l => l.Order).ToList(),
                Settings = currentModule.Settings,
                Version = EntityVersion.IncrementVersion(currentModule.Version)
            };
            
            newModule.MarkAsCreated(currentModule.CreatedBy, DateTimeOffset.UtcNow);
            
            await moduleRepository.CreateAsync(newModule, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
            
            return resDtoMapper.Map<V1ModuleResDto>(newModule);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }*/
    }

    public Task<V1ModuleResDto> UpdateModuleSettingsAsync(string id, V1ModuleSettingsDto settings, User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        /*await unitOfWork.BeginTransactionAsync(cancellationToken);
        
        try
        {
            var currentModule = await moduleRepository.GetLastAsync(id, cancellationToken);
            
            var newModule = new Module(
                id,
                currentModule.CourseId,
                currentModule.Title,
                currentModule.Description,
                currentModule.Order,
                currentModule.Type)
            {
                Lessons = currentModule.Lessons,
                Settings = new ModuleSettings(
                    settings.IsPublished,
                    settings.IsRequired,
                    settings.MinScoreToPass,
                    settings.TimeLimitInMinutes,
                    settings.CustomProperties),
                Version = EntityVersion.IncrementVersion(currentModule.Version)
            };
            
            newModule.MarkAsCreated(user, DateTimeOffset.UtcNow);
            
            await moduleRepository.CreateAsync(newModule, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
            
            return resDtoMapper.Map<V1ModuleResDto>(newModule);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }*/
    }
}