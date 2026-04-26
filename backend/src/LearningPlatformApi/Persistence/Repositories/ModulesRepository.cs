using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.Repositories;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Context;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Repositories.Base.Impl;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformApi.Persistence.Repositories;

public class ModulesRepository(
    ApplicationContext context,
    IDbEntityMapper<Module, string, ModuleEntity, string> moduleMapper,
    ILogger<ModulesRepository> logger)
    : VersionedRepository<Module, string, ModuleEntity, string>(context, moduleMapper, logger),
        IModulesRepository

{
    public override async Task<Module> GetAsync(string id, EntityVersion version,
        CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<ModuleEntity>()
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.IntroductionPage)
            .ThenInclude(x => x.ContentBlocks)
            .Include(x => x.Lessons)
            .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.VersionOrder == version.Order, cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return moduleMapper.Map(entities);
    }

    public new async Task<Module> GetLastAsync(string id, CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<ModuleEntity>()
            .Where(x => x.Id.Equals(id))
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.IntroductionPage)
            .ThenInclude(x => x.ContentBlocks)
            .Include(x => x.Lessons)
            .OrderByDescending(x => x.VersionOrder)
            .FirstOrDefaultAsync(cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return moduleMapper.Map(entities);
    }

    public async Task<IReadOnlyCollection<Module>> GetLastAsync(IReadOnlyCollection<string> ids, CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<ModuleEntity>()
            .Where(x => ids.Contains(x.Id))
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.IntroductionPage)
            .ThenInclude(x => x.ContentBlocks)
            .Include(x => x.Lessons)
            .OrderByDescending(x => x.VersionOrder)
            .Take(ids.Count)
            .ToListAsync(cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return entities.Select(moduleMapper.Map).ToList();
    }
}