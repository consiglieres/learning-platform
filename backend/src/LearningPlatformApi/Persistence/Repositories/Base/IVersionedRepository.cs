using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.ValueObjects;

namespace LearningPlatformApi.Persistence.Repositories.Base;

public interface IVersionedRepository<TVersionableEntity, TId>
    where TVersionableEntity : VersionableEntity<TId>
    where TId : IEquatable<TId>
{
    Task<TVersionableEntity> GetAsync(TId id, EntityVersion version, CancellationToken cancellationToken = default);

    Task<TVersionableEntity> GetLastAsync(TId id, CancellationToken cancellationToken = default);

    Task<TVersionableEntity> CreateAsync(TVersionableEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(TVersionableEntity entity, CancellationToken cancellationToken = default);

    Task<TVersionableEntity> AddNewVersion(TVersionableEntity entity, CancellationToken cancellationToken = default);
}