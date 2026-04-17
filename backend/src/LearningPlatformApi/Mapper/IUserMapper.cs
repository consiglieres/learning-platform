using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.V1.Models.Account.Req;

namespace LearningPlatformApi.Mapper;

public interface IUserMapper
{
    User MapToDomain(UserEntity user);

    UserEntity MapToEntity(User user);

    RegisterUser MapToDomain(V1RegisterUserDto user);
}