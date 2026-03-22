using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformApi.Persistence.Repositories.Base.Impl;

public abstract class AuditableRepository<TDomainEntity, TDomainId, TDbEntity, TDbId>
    : IAuditableRepository<TDomainEntity, TDomainId>
    where TDomainEntity : AuditableEntity<TDomainId>
    where TDomainId : IEquatable<TDomainId>
    where TDbEntity : AuditableDbEntity<TDbId>
    where TDbId : IEquatable<TDbId>
{
    private readonly DbContext context;
    private readonly IDbEntityMapper<TDomainEntity, TDomainId, TDbEntity, TDbId> mapper;
    private readonly ILogger<AuditableRepository<TDomainEntity, TDomainId, TDbEntity, TDbId>> logger;

    protected AuditableRepository(DbContext context,
        IDbEntityMapper<TDomainEntity, TDomainId, TDbEntity, TDbId> mapper,
        ILogger<AuditableRepository<TDomainEntity, TDomainId, TDbEntity, TDbId>> logger)
    {
        this.context = context;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<TDomainEntity> CreateAsync(TDomainEntity entity, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Creating new '{AuditEntity}' Id: '{EntityId}'", typeof(TDomainEntity).Name, entity.Id);
        var mappedEntity = mapper.Map(entity);
        await context.Set<TDbEntity>().AddAsync(mappedEntity);

        return mapper.Map(mappedEntity);
    }

    public async Task<TDomainEntity> UpdateAsync(TDomainEntity entity, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Updating '{AuditEntity}' Id: '{EntityId}'", typeof(TDomainEntity).Name, entity.Id);
        var dbId = mapper.MapId(entity.Id);

        var dbEntity = await context.Set<TDbEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(dbId), cancellationToken: cancellationToken);

        if (dbEntity == null)
            throw new DomainException("Entity not found");

        var updated = mapper.Map(entity);
        context.Entry(dbEntity).CurrentValues.SetValues(updated);

        return mapper.Map(dbEntity);
    }

    public async Task DeleteAsync(TDomainEntity entity, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Deleting entity '{AuditEntity}' Id: '{EntityId}'", typeof(TDomainEntity).Name, entity.Id);

        var mappedEntity = mapper.Map(entity);
        var dbEntity = await context.Set<TDbEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(mappedEntity.Id), cancellationToken: cancellationToken);

        if (dbEntity == null)
            return;

        context.Set<TDbEntity>().Remove(dbEntity);
    }

    public async Task DeleteAsync(TDomainId id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Deleting entity '{AuditEntity}' Id: '{EntityId}'", typeof(TDomainEntity).Name, id);
        var dbId = mapper.MapId(id);
        var dbEntity = await context.Set<TDbEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(dbId), cancellationToken: cancellationToken);

        if (dbEntity == null)
            return;

        context.Set<TDbEntity>().Remove(dbEntity);
    }
}