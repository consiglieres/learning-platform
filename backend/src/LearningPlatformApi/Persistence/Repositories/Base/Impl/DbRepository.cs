using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformApi.Persistence.Repositories.Base.Impl;

public abstract class DbRepository<TDomainEntity, TDomainId, TDbEntity, TDbId>
    : IDbRepository<TDomainEntity, TDomainId>
    where TDomainEntity : DomainEntity<TDomainId>
    where TDomainId : IEquatable<TDomainId>
    where TDbEntity : DbEntity<TDbId>
    where TDbId : IEquatable<TDbId>
{
    private readonly DbContext context;
    private readonly IDbEntityMapper<TDomainEntity, TDomainId, TDbEntity, TDbId> mapper;
    private readonly ILogger<DbRepository<TDomainEntity, TDomainId, TDbEntity, TDbId>> logger;

    protected DbRepository(DbContext context,
        IDbEntityMapper<TDomainEntity, TDomainId, TDbEntity, TDbId> mapper,
        ILogger<DbRepository<TDomainEntity, TDomainId, TDbEntity, TDbId>> logger)
    {
        this.context = context;
        this.mapper = mapper;
        this.logger = logger;
    }

    public virtual async Task<TDomainEntity?> GetByIdAsync(TDomainId id, CancellationToken cancellationToken = default)
    {
        var dbId = mapper.MapId(id);
        var dbEntity = await context.Set<TDbEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(dbId), cancellationToken);

        return dbEntity != null ? mapper.Map(dbEntity) : null;
    }

    public virtual async Task<TDomainEntity> CreateAsync(TDomainEntity entity, CancellationToken cancellationToken = default)
    {
        var dbEntity = mapper.Map(entity);
        await context.Set<TDbEntity>().AddAsync(dbEntity, cancellationToken);

        return mapper.Map(dbEntity);
    }

    public virtual async Task DeleteAsync(TDomainEntity entity, CancellationToken cancellationToken = default)
    {
        var dbId = mapper.MapId(entity.Id);
        var dbEntity = await context.Set<TDbEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(dbId), cancellationToken);

        if (dbEntity == null)
            return;

        context.Set<TDbEntity>().Remove(dbEntity);
    }

    public virtual async Task DeleteAsync(TDomainId id, CancellationToken cancellationToken = default)
    {
        var dbId = mapper.MapId(id);
        var dbEntity = await context.Set<TDbEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(dbId), cancellationToken);

        if (dbEntity == null)
            return;

        context.Set<TDbEntity>().Remove(dbEntity);
    }

    public virtual async Task<TDomainEntity> UpdateAsync(TDomainEntity entity, CancellationToken cancellationToken = default)
    {
        var dbId = mapper.MapId(entity.Id);
        var dbEntity = await context.Set<TDbEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(dbId), cancellationToken);

        if (dbEntity == null)
            throw new DomainException("Update exception - couldn't update, entity not found");

        var updated = mapper.Map(entity);
        context.Entry(dbEntity).CurrentValues.SetValues(updated);

        return mapper.Map(dbEntity);
    }
}