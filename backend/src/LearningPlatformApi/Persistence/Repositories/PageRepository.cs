using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.Repositories;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Context;
using LearningPlatformApi.Persistence.Entities.Page;
using LearningPlatformApi.Persistence.Repositories.Base.Impl;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformApi.Persistence.Repositories;

public sealed class PageRepository(ApplicationContext context,
    IDbEntityMapper<Page, string, PageEntity, string> courseMapper,
    IDbEntityMapper<PageContentBlock, string, ContentBlockEntity, string> contentBlockMapper,
    ILogger<PageRepository> logger)
    : AuditableRepository<Page, string, PageEntity, string>(context, courseMapper, logger), IPageRepository
{
    public override async Task<Page> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<PageEntity>()
            .Where(x => x.Id.Equals(id))
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Include(x => x.ContentBlocks)
            .FirstOrDefaultAsync(cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return courseMapper.Map(entities);
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
            context.Set<ContentBlockEntity>().Add(block);
        }

        dbEntity.ContentBlocks = dbBlocks;
        await context.Set<PageEntity>().AddAsync(dbEntity, cancellationToken);
    }
}