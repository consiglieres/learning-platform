using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.Repositories;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Repositories.Base;
using LearningPlatformApi.V1.Mapper;
using LearningPlatformApi.V1.Models.Base;
using LearningPlatformApi.V1.Models.Module.Req;
using LearningPlatformApi.V1.Models.Module.Res;

namespace LearningPlatformApi.Services.Impl;

public class ModuleService(IModulesRepository moduleRepository, IV1ResDtoMapper resDtoMapper,
    IUnitOfWork unitOfWork, IDbEntityMapper<Module, string, ModuleEntity, string> moduleMapper,
    IDbEntityMapper<Lesson, string, LessonEntity, string> lessonMapper) : IModuleService
{
    public async Task<V1ModuleResDto> CreateAsync(CreateModuleRequest request, User user, CancellationToken cancellationToken)
    {
        var module = new Module(Guid.NewGuid().ToString(), request.ModuleOrder,
            request.CourseId, user, []);

        await moduleRepository.CreateAsync(module, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var createdModule = await moduleRepository.GetLastAsync(module.Id, cancellationToken);
        return resDtoMapper.Map(createdModule);
    }

    public async Task<V1ModuleResDto> UpdateAsync(string id, User user, UpdateModuleRequest request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var currentModule = await moduleRepository.GetLastAsync(id, cancellationToken);

            if (currentModule == null)
                throw new DomainException($"Module with id {id} not found");

            var newModule = new Module(
                request.Name,
                request.ModuleOrder,
                currentModule.CourseId,
                user,
                currentModule.Lessons);

            newModule.MarkAsCreated(user, DateTimeOffset.UtcNow);
            newModule.Version = EntityVersion.IncrementVersion(currentModule.Version);

            await moduleRepository.CreateAsync(newModule, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            var createdModule = await moduleRepository.GetLastAsync(newModule.Id, cancellationToken);
            return resDtoMapper.Map(createdModule);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task<V1ModuleResDto> GetLatestAsync(string id, CancellationToken cancellationToken)
    {
        var module = await moduleRepository.GetLastAsync(id, cancellationToken);
        return resDtoMapper.Map(module);
    }

    public async Task<V1ModuleResDto> GetByVersionAsync(string id, int versionOrder, CancellationToken cancellationToken)
    {
        var module = await moduleRepository.GetAsync(id, new EntityVersion(versionOrder), cancellationToken);
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

    public async Task<V1ModuleResDto> RestoreAsync(string id, User user, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var deletedModule = await moduleRepository.GetLastAsync(id, cancellationToken);

            if (deletedModule == null)
                throw new DomainException($"No deleted version found for module {id}");

            var restoredModule = new Module(
                deletedModule.Name,
                deletedModule.ModuleOrder,
                deletedModule.CourseId,
                user,
                deletedModule.Lessons)
            {
                Version = EntityVersion.IncrementVersion(deletedModule.Version)
            };

            restoredModule.MarkAsCreated(user, DateTimeOffset.UtcNow);
            restoredModule.Restore(user, DateTimeOffset.UtcNow);

            await moduleRepository.CreateAsync(restoredModule, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return resDtoMapper.Map(restoredModule);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task<V1ModuleResDto> RollbackToVersionAsync(string id, int targetVersionOrder, string? reason,
        CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var targetModule = await moduleRepository.GetAsync(id, new EntityVersion(targetVersionOrder), cancellationToken);
            var currentModule = await moduleRepository.GetLastAsync(id, cancellationToken);

            if (targetVersionOrder >= currentModule.Version.Order)
                throw new DomainException("Cannot rollback to current or future version");

            var rolledBackModule = targetModule with
            {
                Version = new EntityVersion(currentModule.Version.Order + 1, Guid.NewGuid().ToString())
            };

            rolledBackModule.Lessons = targetModule.Lessons.Select(lesson => lesson with
            {
                Id = Guid.NewGuid().ToString(),
                ModuleId = rolledBackModule.Id
            }).ToList();

            rolledBackModule.MarkAsCreated(currentModule.CreatedBy, DateTimeOffset.UtcNow);

            await moduleRepository.CreateAsync(rolledBackModule, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return resDtoMapper.Map(rolledBackModule);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<ModuleVersionInfoDto>> GetVersionHistoryAsync(string id, int limit, CancellationToken cancellationToken)
    {
        var allVersions = await moduleRepository.GetAllVersionsAsync(id, cancellationToken);

        var history = allVersions
            .OrderByDescending(m => m.Version.Order)
            .Take(limit)
            .Select(version => new ModuleVersionInfoDto
            {
                Version = new VersionDto
                {
                    Order = version.Version.Order,
                    Tag = version.Version.Tag
                },
                CreatedAt = version.CreatedAt,
                CreatedBy = resDtoMapper.Map(version.CreatedBy),
                LessonsCount = version.Lessons.Count,
                ChangeDescription = GetChangeDescription(version, allVersions)
            })
            .ToList();

        return history;
    }

    public async Task<ModuleComparisonResDto> CompareVersionsAsync(string id, int sourceVersion, int targetVersion, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        /*var sourceModule = await moduleRepository.GetAsync(id, new EntityVersion(sourceVersion), cancellationToken);
        var targetModule = await moduleRepository.GetAsync(id, new EntityVersion(targetVersion), cancellationToken);
        
        var sourceLessons = sourceModule.Lessons.ToDictionary(l => l.Id, l => l);
        var targetLessons = targetModule.Lessons.ToDictionary(l => l.Id, l => l);
        
        var addedLessons = new List<LessonDiffDto>();
        var removedLessons = new List<LessonDiffDto>();
        var modifiedLessons = new List<LessonDiffDto>();
        var propertyChanges = new List<ModuleComparisonResDto>();
        
        foreach (var (targetLessonId, targetLesson) in targetLessons)
        {
            if (sourceLessons.TryGetValue(id, out var sourceLesson))
            {
                if (sourceLesson.Title != targetLesson.Title ||
                    sourceLesson.Content != targetLesson.Content ||
                    sourceLesson.Duration != targetLesson.Duration)
                {
                    modifiedLessons.Add(new LessonDiffDto
                    {
                        LessonId = id,
                        Title = targetLesson.Title,
                        Order = targetLesson.Order,
                        Type = targetLesson.Type,
                        OldContent = sourceLesson.Content,
                        NewContent = targetLesson.Content
                    });
                }
            }
            else
            {
                addedLessons.Add(new LessonDiffDto
                {
                    LessonId = id,
                    Title = targetLesson.Title,
                    Order = targetLesson.Order,
                    Type = targetLesson.Type,
                    NewContent = targetLesson.Content
                });
            }
        }
        
        // Find removed lessons
        foreach (var (id, sourceLesson) in sourceLessons)
        {
            if (!targetLessons.ContainsKey(id))
            {
                removedLessons.Add(new LessonDiffDto
                {
                    LessonId = id,
                    Title = sourceLesson.Title,
                    Order = sourceLesson.Order,
                    Type = sourceLesson.Type,
                    OldContent = sourceLesson.Content
                });
            }
        }
        
        // Check property changes
        if (sourceModule.Title != targetModule.Title)
            propertyChanges.Add(new PropertyChangeDto { Property = "Title", OldValue = sourceModule.Title, NewValue = targetModule.Title });
        
        if (sourceModule.Description != targetModule.Description)
            propertyChanges.Add(new PropertyChangeDto { Property = "Description", OldValue = sourceModule.Description, NewValue = targetModule.Description });
        
        if (sourceModule.Order != targetModule.Order)
            propertyChanges.Add(new PropertyChangeDto { Property = "Order", OldValue = sourceModule.Order.ToString(), NewValue = targetModule.Order.ToString() });
        
        if (sourceModule.Type != targetModule.Type)
            propertyChanges.Add(new PropertyChangeDto { Property = "Type", OldValue = sourceModule.Type.ToString(), NewValue = targetModule.Type.ToString() });
        
        return new ModuleComparisonResDto
        {
            SourceVersion = resDtoMapper.Map<V1ModuleResDto>(sourceModule),
            TargetVersion = resDtoMapper.Map<V1ModuleResDto>(targetModule),
            Differences = new ModuleDiffDto
            {
                AddedLessons = addedLessons,
                RemovedLessons = removedLessons,
                ModifiedLessons = modifiedLessons,
                PropertyChanges = propertyChanges
            }
        };*/
    }

    public async Task<V1ModuleResDto> CopyModuleAsync(CopyModuleRequest request, User user, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var sourceModule = request.SourceVersionOrder.HasValue
                ? await moduleRepository.GetAsync(request.SourceModuleId, new EntityVersion(request.SourceVersionOrder.Value), cancellationToken)
                : await moduleRepository.GetLastAsync(request.SourceModuleId, cancellationToken);

            var newModule = sourceModule with
            {
                Id = Guid.NewGuid().ToString(),
                CourseId = request.TargetCourseId,
                Name = request.NewModuleName ?? sourceModule.Name,
                ModuleOrder = request.NewModuleOrder,
                Version = EntityVersion.CreateDefault()
            };

            newModule.Lessons = sourceModule.Lessons.Select(lesson => lesson with
            {
                ModuleId = newModule.Id
            }).ToList();

            newModule.MarkAsCreated(user, DateTimeOffset.UtcNow);

            await moduleRepository.CreateAsync(newModule, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return resDtoMapper.Map(newModule);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<V1ModuleResDto>> GetModulesByIdsAsync(List<string> ids, CancellationToken cancellationToken)
    {
        var modules = await moduleRepository.GetLastAsync(ids, cancellationToken);
        return modules.Select(resDtoMapper.Map).ToList();
    }

    public async Task<V1ModuleResDto> ReorderLessonsAsync(string id, List<string> lessonIds, CancellationToken cancellationToken)
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

    private string GetChangeDescription(Module module, IReadOnlyCollection<Module> allVersions)
    {
        var previousVersion = allVersions
            .FirstOrDefault(m => m.Version.Order == module.Version.Order - 1);

        if (previousVersion == null) return "Initial version";

        var changes = new List<string>();

        if (module.Name != previousVersion.Name)
            changes.Add($"Name changed from '{previousVersion.Name}' to '{module.Name}'");

        if (module.ModuleOrder != previousVersion.ModuleOrder)
            changes.Add($"ModuleOrder changed from {previousVersion.ModuleOrder} to {module.ModuleOrder}");

        var lessonsAdded = module.Lessons.Count - previousVersion.Lessons.Count;
        if (lessonsAdded > 0)
            changes.Add($"{lessonsAdded} lesson(s) added");
        else if (lessonsAdded < 0)
            changes.Add($"{Math.Abs(lessonsAdded)} lesson(s) removed");

        var modifiedLessons = module.Lessons
            .Count(l => previousVersion.Lessons.Any(pl => pl.Id == l.Id &&
                (pl.Name != l.Name || pl.PassThreshold != l.PassThreshold)));

        if (modifiedLessons > 0)
            changes.Add($"{modifiedLessons} lesson(s) modified");

        return changes.Any() ? string.Join(", ", changes) : "No significant changes";
    }
}