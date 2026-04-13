using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformApi.Persistence.Repositories.Base.Impl;

public abstract class AuditableRepository<TDomainEntity, TDomainId, TDbEntity, TDbId>
    : DbRepository<TDomainEntity, TDomainId, TDbEntity, TDbId>, IAuditableRepository<TDomainEntity, TDomainId>
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
        ILogger<AuditableRepository<TDomainEntity, TDomainId, TDbEntity, TDbId>> logger) : base(context, mapper, logger)
    {
        this.context = context;
        this.mapper = mapper;
        this.logger = logger;
    }
    
    public virtual async Task DeleteAsync(TDomainEntity entity,  User user, CancellationToken cancellationToken = default)
    {
        var dbId = mapper.MapId(entity.Id);
        var dbEntity = await context.Set<TDbEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(dbId), cancellationToken);

        if (dbEntity == null)
            return;

        dbEntity.MarkAsDeleted(user, DateTimeOffset.UtcNow);
    }

    public async Task DeleteAsync(TDomainId id,  User user, CancellationToken cancellationToken = default)
    {
        var dbId = mapper.MapId(id);
        var dbEntities = await context.Set<TDbEntity>()
            .Where(x => x.Id.Equals(dbId))
            .ToListAsync(cancellationToken: cancellationToken);

        if (!dbEntities.Any())
            return;

        foreach(var dbEntity in dbEntities)
        {
            dbEntity.MarkAsDeleted(user, DateTimeOffset.UtcNow);
        }

        context.RemoveRange(dbEntities);
    }
}