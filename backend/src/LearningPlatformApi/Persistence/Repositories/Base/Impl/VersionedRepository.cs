using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformApi.Persistence.Repositories.Base.Impl;

public class VersionedRepository<TVersionableEntity, TDomainId, TVersionableDbEntity, TDbId>
    : IVersionedRepository<TVersionableEntity, TDomainId>
    where TVersionableEntity : VersionableEntity<TDomainId>
    where TDomainId : IEquatable<TDomainId>
    where TVersionableDbEntity : VersionableDbEntity<TDbId>
    where TDbId : IEquatable<TDbId>
{
    private readonly DbContext context;
    private readonly IDbEntityMapper<TVersionableEntity, TDomainId, TVersionableDbEntity, TDbId> pageMapper;

    protected VersionedRepository(DbContext context,
        IDbEntityMapper<TVersionableEntity, TDomainId, TVersionableDbEntity, TDbId> pageMapper,
        ILogger<VersionedRepository<TVersionableEntity, TDomainId, TVersionableDbEntity, TDbId>> logger)
    {
        this.context = context;
        this.pageMapper = pageMapper;
    }

    public virtual async Task<TVersionableEntity> GetAsync(TDomainId id, EntityVersion version,
        CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<TVersionableDbEntity>()
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.VersionOrder == version.Order, cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return pageMapper.Map(entities);
    }

    public virtual async Task<IReadOnlyCollection<TVersionableEntity>> GetAllVersionsAsync(TDomainId id, 
        CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<TVersionableDbEntity>()
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .Where(x => x.Id.Equals(id))
            .ToListAsync(cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return entities.Select(pageMapper.Map).ToList();
    }

    public virtual async Task<TVersionableEntity> GetLastAsync(TDomainId id, CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<TVersionableDbEntity>()
            .Where(x => x.Id.Equals(id))
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.DeletedByUser)
            .OrderByDescending(x => x.VersionOrder)
            .LastOrDefaultAsync(cancellationToken);

        if (entities == null) throw new DomainException("Entity not found");

        return pageMapper.Map(entities);
    }

    public virtual async Task CreateAsync(TVersionableEntity entity, CancellationToken cancellationToken = default)
    {
        var dbEntity = pageMapper.Map(entity);
        await context.Set<TVersionableDbEntity>().AddAsync(dbEntity, cancellationToken);
    }

    public virtual async Task DeleteAsync(TVersionableEntity entity, User user, CancellationToken cancellationToken = default)
    {
        var dbId = pageMapper.MapId(entity.Id);
        var dbEntity = await context.Set<TVersionableDbEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(dbId) && x.VersionOrder == entity.Version.Order, cancellationToken);

        if (dbEntity == null)
            return;

        dbEntity.MarkAsDeleted(user, DateTimeOffset.UtcNow);
    }

    public virtual async Task DeleteAsync(TDomainId id, User user, CancellationToken cancellationToken = default)
    {
        var dbId = pageMapper.MapId(id);
        var dbEntities = await context.Set<TVersionableDbEntity>()
            .Where(x => x.Id.Equals(dbId))
            .ToListAsync(cancellationToken);

        if (!dbEntities.Any())
            return;

        foreach (var dbEntity in dbEntities) dbEntity.MarkAsDeleted(user, DateTimeOffset.UtcNow);
    }

    public virtual async Task<TVersionableEntity> UpdateAsync(TVersionableEntity entity,
        CancellationToken cancellationToken = default)
    {
        var dbId = pageMapper.MapId(entity.Id);
        var dbEntity = await context.Set<TVersionableDbEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(dbId) && x.VersionOrder == entity.Version.Order,
                cancellationToken);

        if (dbEntity == null) throw new DomainException("Entity not found");

        var updated = pageMapper.Map(entity);
        context.Entry(dbEntity).CurrentValues.SetValues(updated);
        return pageMapper.Map(dbEntity);
    }

    public virtual async Task AddNewVersion(TVersionableEntity entity, CancellationToken cancellationToken = default)
    {
        var lastEntity = await context.Set<TVersionableDbEntity>()
            .Where(x => x.Id.Equals(pageMapper.MapId(entity.Id)))
            .OrderByDescending(x => x.VersionOrder)
            .LastOrDefaultAsync(cancellationToken);

        if (lastEntity != null && lastEntity.VersionOrder >= entity.Version.Order)
            throw new DomainException("Entity is already created");
    }
}