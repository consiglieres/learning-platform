using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.Repositories;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Context;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Entities.Page;
using LearningPlatformApi.Persistence.Repositories.Base.Impl;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformApi.Persistence.Repositories;

public class ModulesRepository(ApplicationContext context,
    IDbEntityMapper<Module, string, ModuleEntity, string> moduleMapper, ILogger<ModulesRepository> logger)
    : AuditableRepository<Module, string, ModuleEntity, string>(context, moduleMapper, logger),
        IModulesRepository

{
    public override async Task<Module> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<ModuleEntity>()
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.Page)
            .ThenInclude(x => x.ContentBlocks)
            .Include(x => x.Lessons)
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

        var lessons = await context.Set<LessonEntity>()
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.PageEntity)
            .ThenInclude(x => x.ContentBlocks)
            .Where(x => x.ModuleId == id)
            .ToListAsync(cancellationToken);

        foreach (var lesson in lessons)
        {
            var testTasks = await context.Set<TestTaskEntity>()
                .Include(x => x.CreatedByUser)
                .Include(x => x.UpdatedByUser)
                .Include(x => x.DeletedByUser)
                .Include(x => x.Page)
                .ThenInclude(x => x.ContentBlocks)
                .ToListAsync(cancellationToken);
            
            var codingTasks = await context.Set<CodingTaskEntity>()
                .Include(x => x.CreatedByUser)
                .Include(x => x.UpdatedByUser)
                .Include(x => x.DeletedByUser)
                .Include(x => x.Page)
                .ThenInclude(x => x.ContentBlocks)
                .ToListAsync(cancellationToken);
            lesson.CodingTasks = codingTasks;
            lesson.TestTasks = testTasks;
        }
        
        if (entities == null) throw new DomainException("Entity not found");

        return moduleMapper.Map(entities);
    }

    public async Task<IReadOnlyCollection<Module>> GetByIdsAsync(IReadOnlyCollection<string> ids,
        CancellationToken cancellationToken = default)
    {
        var entities = new List<Module>();
        
        foreach (var id in ids)
        {
            var module = await GetByIdAsync(id, cancellationToken);
            entities.Add(module);
        }
        
        return entities;
    }
    
    public override async Task<Module> UpdateAsync(Module entity, CancellationToken cancellationToken = default)
    {
        var dbId = entity.Id;
        var dbEntity = await context.Set<ModuleEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(dbId), cancellationToken);

        if (dbEntity == null) throw new DomainException("Entity not found");

        dbEntity.Name = entity.Name;
        dbEntity.ModuleOrder = entity.ModuleOrder;
        await context.SaveChangesAsync(cancellationToken);

        return await GetByIdAsync(entity.Id, cancellationToken);
    }

    public override async Task CreateAsync(Module entity, CancellationToken cancellationToken = default)
    {
        var moduleEntity = moduleMapper.Map(entity);
        var page = await context.Set<PageEntity>()
            .FirstOrDefaultAsync(x => x.Id == moduleEntity.Page.Id, cancellationToken);

        if (page != null)
        {
            moduleEntity.Page = page;
        }
        await context.Set<ModuleEntity>().AddAsync(moduleEntity, cancellationToken);
    }
}