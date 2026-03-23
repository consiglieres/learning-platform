using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Exceptions;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformApi.Persistence.Repositories.Base.Impl;

public abstract class PublicationWorkflowRepository<TPublicationEntity, TPublicationId, TDbEntity, TDbId>
    : AuditableRepository<TPublicationEntity, TPublicationId, TDbEntity, TDbId>, IPublicationWorkflowRepository<TPublicationEntity, TPublicationId>
    where TPublicationEntity : PublicationWorkflowEntity<TPublicationId>
    where TPublicationId : IEquatable<TPublicationId>
    where TDbEntity : PublicationDbEntity<TDbId>
    where TDbId : IEquatable<TDbId>
{
    private readonly DbContext context;
    private readonly IDbEntityMapper<TPublicationEntity, TPublicationId, TDbEntity, TDbId> mapper;
    private readonly ILogger<AuditableRepository<TPublicationEntity, TPublicationId, TDbEntity, TDbId>> logger;

    protected PublicationWorkflowRepository(DbContext context,
        IDbEntityMapper<TPublicationEntity, TPublicationId, TDbEntity, TDbId> mapper,
        ILogger<PublicationWorkflowRepository<TPublicationEntity, TPublicationId, TDbEntity, TDbId>> logger)
        : base(context, mapper, logger)
    {
        this.context = context;
        this.mapper = mapper;
        this.logger = logger;
    }
}