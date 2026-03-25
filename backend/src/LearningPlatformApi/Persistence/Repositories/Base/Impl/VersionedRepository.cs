using LearningPlatformApi.Domain.Base.Impl;
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
    private readonly IDbEntityMapper<TVersionableEntity, TDomainId, TVersionableDbEntity, TDbId> mapper;
    private readonly ILogger<VersionedRepository<TVersionableEntity, TDomainId, TVersionableDbEntity, TDbId>> logger;

    protected VersionedRepository(DbContext context,
        IDbEntityMapper<TVersionableEntity, TDomainId, TVersionableDbEntity, TDbId> mapper,
        ILogger<VersionedRepository<TVersionableEntity, TDomainId, TVersionableDbEntity, TDbId>> logger)
    {
        this.context = context;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<TVersionableEntity> GetAsync(TDomainId id, EntityVersion version, CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<TVersionableDbEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.Order == version.Order, cancellationToken: cancellationToken);

        if(entities == null) throw new DomainException("Entity not found");
        
        return mapper.Map(entities);
    }

    public async Task<TVersionableEntity> GetLastAsync(TDomainId id, CancellationToken cancellationToken = default)
    {
        var entities = await context.Set<TVersionableDbEntity>()
            .Where(x => x.Id.Equals(id))
            .OrderByDescending(x => x.Order)
            .LastOrDefaultAsync(cancellationToken: cancellationToken);

        if(entities == null) throw new DomainException("Entity not found");
        
        return mapper.Map(entities);
    }

    public virtual async Task<TVersionableEntity> CreateAsync(TVersionableEntity entity, CancellationToken cancellationToken = default)
    {
        var dbEntity = mapper.Map(entity);
        await context.Set<TVersionableDbEntity>().AddAsync(dbEntity, cancellationToken);

        return mapper.Map(dbEntity);
    }

    public virtual async Task DeleteAsync(TVersionableEntity entity, CancellationToken cancellationToken = default)
    {
        var dbId = mapper.MapId(entity.Id);
        var dbEntity = await context.Set<TVersionableDbEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(dbId) && x.Order == entity.Version.Order, cancellationToken);
        
        if (dbEntity == null)
            return;
        
        var dbEntityDeleted = mapper.Map(dbEntity);
        context.Entry(dbEntity).CurrentValues.SetValues(dbEntityDeleted);
    }

    public virtual async Task<TVersionableEntity> AddNewVersion(TVersionableEntity entity, CancellationToken cancellationToken = default)
    {
        var lastEntity = await context.Set<TVersionableDbEntity>()
            .Where(x => x.Id.Equals(mapper.MapId(entity.Id)))
            .OrderByDescending(x => x.Order)
            .LastOrDefaultAsync(cancellationToken: cancellationToken);

        if (lastEntity != null && lastEntity.Order >= entity.Version.Order)
        {
            throw new DomainException("Entity is already created");
        }
        
        var created = await CreateAsync(entity, cancellationToken);
        return created;
    }
}