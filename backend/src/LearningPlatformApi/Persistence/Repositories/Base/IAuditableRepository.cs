using LearningPlatformApi.Domain.Base.Impl;

namespace LearningPlatformApi.Persistence.Repositories.Base;

public interface IAuditableRepository<TDomainEntity, TId>
    where TDomainEntity : AuditableEntity<TId>
    where TId : IEquatable<TId>
{
    Task<TDomainEntity> CreateAsync(TDomainEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(TDomainEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(TId id, CancellationToken cancellationToken = default);

    Task<TDomainEntity> UpdateAsync(TDomainEntity entity, CancellationToken cancellationToken = default);
}