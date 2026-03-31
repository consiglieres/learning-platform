using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Entities;

namespace LearningPlatformApi.Persistence.Repositories.Base;

public interface IPublicationWorkflowRepository<TPublicationEntity, TId>
    : IVersionedRepository<TPublicationEntity, TId>
    where TPublicationEntity : PublicationWorkflowEntity<TId>
    where TId : IEquatable<TId>
{
}