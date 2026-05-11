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

        if (entities == null) throw new DomainException("Entity not found");

        return moduleMapper.Map(entities);
    }

    public async Task<IReadOnlyCollection<Module>> GetByIdsAsync(IReadOnlyCollection<string> ids,
        CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<ModuleEntity>()
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.Page)
            .ThenInclude(x => x.ContentBlocks)
            .Include(x => x.Lessons)
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return entities.Select(moduleMapper.Map).ToList();
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