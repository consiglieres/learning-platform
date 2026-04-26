using LearningPlatformApi.Persistence.Entities.Base;

namespace LearningPlatformApi.Mapper;

public interface IDtoMapper<TDomainEntity, TDomainId>
    where TDomainEntity : DbEntity<TDomainId>
{
}