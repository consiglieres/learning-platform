using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformApi.Persistence.Repositories.Base.Impl;

public abstract class PublicationWorkflowRepository<TPublicationEntity, TPublicationId, TDbEntity, TDbId>
    : VersionedRepository<TPublicationEntity, TPublicationId, TDbEntity, TDbId>,
        IPublicationWorkflowRepository<TPublicationEntity, TPublicationId>
    where TPublicationEntity : PublicationWorkflowEntity<TPublicationId>
    where TPublicationId : IEquatable<TPublicationId>
    where TDbEntity : PublicationDbEntity<TDbId>
    where TDbId : IEquatable<TDbId>
{
    protected PublicationWorkflowRepository(DbContext context,
        IDbEntityMapper<TPublicationEntity, TPublicationId, TDbEntity, TDbId> pageMapper,
        ILogger<PublicationWorkflowRepository<TPublicationEntity, TPublicationId, TDbEntity, TDbId>> logger)
        : base(context, pageMapper, logger)
    {
    }
}