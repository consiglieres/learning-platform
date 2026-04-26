using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.V2.Account.Req;

namespace LearningPlatformApi.Mapper;

public interface IUserMapper
{
    User MapToDomain(UserEntity user);

    UserEntity MapToEntity(User user);

    RegisterUser MapToDomain(V1RegisterUserDto user);

    RegisterUser MapToDomain(V2RegisterUserDto user);
}