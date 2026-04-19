using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.Repositories;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Context;
using LearningPlatformApi.Persistence.Entities.Page;
using LearningPlatformApi.Persistence.Repositories.Base.Impl;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformApi.Persistence.Repositories;

public sealed class PageRepository(
    ApplicationContext context,
    IDbEntityMapper<Page, string, PageEntity, string> courseMapper,
    IDbEntityMapper<PageContentBlock, string, ContentBlockEntity, string> contentBlockMapper,
    ILogger<PageRepository> logger)
    : VersionedRepository<Page, string, PageEntity, string>(context, courseMapper, logger), IPageRepository
{
    public override async Task<Page> GetAsync(string id, EntityVersion version,
        CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<PageEntity>()
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.ContentBlocks)
            .OrderByDescending(x => x.VersionOrder)
            .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.VersionOrder == version.Order, cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return courseMapper.Map(entities);
    }

    public override async Task<Page> GetLastAsync(string id, CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<PageEntity>()
            .Where(x => x.Id.Equals(id))
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.ContentBlocks)
            .OrderByDescending(x => x.VersionOrder)
            .FirstOrDefaultAsync(cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return courseMapper.Map(entities);
    }

    public override async Task<IReadOnlyCollection<Page>> GetAllVersionsAsync(string id,
        CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<PageEntity>()
            .Where(x => x.Id.Equals(id))
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.ContentBlocks)
            .ToListAsync(cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return entities.Select(courseMapper.Map).ToList();
    }

    public override async Task<Page> UpdateAsync(Page entity,
        CancellationToken cancellationToken = default)
    {
        var dbId = entity.Id;
        var dbEntity = await context.Set<PageEntity>()
            .Include(x => x.ContentBlocks)
            .FirstOrDefaultAsync(x => x.Id.Equals(dbId) && x.VersionOrder == entity.Version.Order,
                cancellationToken);

        if (dbEntity == null) throw new DomainException("Entity not found");

        entity.Version = EntityVersion.IncrementVersion(entity.Version);
        var updated = courseMapper.Map(entity);
        context.Entry(dbEntity).CurrentValues.SetValues(updated);
        dbEntity.VersionOrder = entity.Version.Order;
        dbEntity.Tag = entity.Version.Tag;

        foreach (var existingBlock in dbEntity.ContentBlocks.ToList())
        {
            dbEntity.ContentBlocks.Remove(existingBlock);
            context.Entry(existingBlock).State = EntityState.Deleted;
        }

        foreach (var newBlock in entity.ContentBlocks)
        {
            var dbBlock = contentBlockMapper.Map(newBlock);
            dbEntity.ContentBlocks.Add(dbBlock);
        }

        await context.SaveChangesAsync(cancellationToken);

        return courseMapper.Map(dbEntity);
    }
}