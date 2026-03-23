using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Entities;

namespace LearningPlatformApi.Persistence.Repositories.Base;

public interface IPublicationWorkflowRepository<TPublicationEntity, TId>
    : IAuditableRepository<TPublicationEntity, TId>
    where TPublicationEntity : PublicationWorkflowEntity<TId>
    where TId : IEquatable<TId>
{
}