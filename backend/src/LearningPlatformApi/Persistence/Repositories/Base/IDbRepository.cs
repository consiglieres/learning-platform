using LearningPlatformApi.Domain.Base.Impl;

namespace LearningPlatformApi.Persistence.Repositories.Base;

public interface IDbRepository<TDomainEntity, TId>
    where TDomainEntity : DomainEntity<TId>
    where TId : IEquatable<TId>
{
    Task<TDomainEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task CreateAsync(TDomainEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(TDomainEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(TId id, CancellationToken cancellationToken = default);

    Task<TDomainEntity> UpdateAsync(TDomainEntity entity, CancellationToken cancellationToken = default);
}