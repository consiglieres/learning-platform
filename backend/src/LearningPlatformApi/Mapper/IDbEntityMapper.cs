using LearningPlatformApi.Domain.Base.Impl;
using LearningPlatformApi.Persistence.Entities.Base;

namespace LearningPlatformApi.Mapper;

public interface IDbEntityMapper<TDomainEntity, TDomainId, TDbEntity, TDbEntityId>
    where TDomainEntity : DomainEntity<TDomainId>
    where TDbEntity : DbEntity<TDbEntityId>
{
    TDomainEntity Map(TDbEntity entity);

    TDbEntity Map(TDomainEntity entity);

    TDomainId MapId(TDbEntityId id);

    TDbEntityId MapId(TDomainId id);
}