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

    public override Task<Page> UpdateAsync(Page entity,
        CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException("Use CreateAsync for versioned entities. Update is not supported.");
    }

    public override async Task CreateAsync(Page entity, CancellationToken cancellationToken = default)
    {
        var dbEntity = courseMapper.Map(entity);

        var dbBlocks = entity.ContentBlocks
            .Select(contentBlockMapper.Map)
            .ToList();

        foreach (var block in dbBlocks)
        {
            block.PageId = dbEntity.Id;
            block.PageVersion = dbEntity.VersionOrder;
            context.Set<ContentBlockEntity>().Add(block);
        }

        dbEntity.ContentBlocks = dbBlocks;
        await context.Set<PageEntity>().AddAsync(dbEntity, cancellationToken);
    }
}