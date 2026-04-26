using LearningPlatformApi.Domain.Base.Impl;

namespace LearningPlatformApi.Persistence.Repositories.Base;

public interface IPublicationWorkflowRepository<TPublicationEntity, TId>
    : IVersionedRepository<TPublicationEntity, TId>
    where TPublicationEntity : PublicationWorkflowEntity<TId>
    where TId : IEquatable<TId>
{
}