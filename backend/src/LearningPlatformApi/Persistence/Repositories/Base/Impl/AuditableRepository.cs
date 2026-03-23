using LearningPlatformApi.Domain.Base.Impl;
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
}