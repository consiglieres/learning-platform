using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Domain.Entities;

namespace LearningPlatformApi.Persistence.Repositories.Base;

public interface IAuditableRepository<TDomainEntity, TId>
    where TDomainEntity : AuditableEntity<TId>
    where TId : IEquatable<TId>
{
    Task<TDomainEntity?> FindByIdAsync(TId id, CancellationToken cancellationToken = default);
    
    Task<TDomainEntity> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    
    Task CreateAsync(TDomainEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(TDomainEntity entity, User user, CancellationToken cancellationToken = default);

    Task DeleteAsync(TId id, User user, CancellationToken cancellationToken = default);

    Task<TDomainEntity> UpdateAsync(TDomainEntity entity, CancellationToken cancellationToken = default);
}