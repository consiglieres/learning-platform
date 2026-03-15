using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.V1.Models.Req;

namespace LearningPlatformApi.Mapper;

public interface IUserMapper
{
    DomainUser MapToDomain(UserEntity user);

    UserEntity MapToEntity(DomainUser user);

    RegisterUser MapToDomain(V1RegisterUserDto user);
}