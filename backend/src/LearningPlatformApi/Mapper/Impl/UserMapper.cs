using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.V1.Models.Req;
using Riok.Mapperly.Abstractions;

namespace LearningPlatformApi.Mapper.Impl;

[Mapper]
internal partial class UserMapper : IUserMapper
{
    public partial DomainUser MapToDomain(UserEntity user);

    public partial UserEntity MapToEntity(DomainUser user);

    public partial RegisterUser MapToDomain(V1RegisterUserDto user);
}