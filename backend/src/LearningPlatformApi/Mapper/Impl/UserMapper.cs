using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.V1.Models.Account.Req;
using Riok.Mapperly.Abstractions;

namespace LearningPlatformApi.Mapper.Impl;

[Mapper]
internal partial class UserMapper : IUserMapper
{
    [MapperIgnoreSource(nameof(UserEntity.PhoneNumber))]
    [MapperIgnoreSource(nameof(UserEntity.PhoneNumberConfirmed))]
    [MapperIgnoreSource(nameof(UserEntity.NormalizedUserName))]
    [MapperIgnoreSource(nameof(UserEntity.UserResources))]
    public partial User MapToDomain(UserEntity user);

    [MapperIgnoreTarget(nameof(UserEntity.PhoneNumber))]
    [MapperIgnoreTarget(nameof(UserEntity.PhoneNumberConfirmed))]
    [MapperIgnoreTarget(nameof(UserEntity.UserResources))]
    public partial UserEntity MapToEntity(User user);

    public partial RegisterUser MapToDomain(V1RegisterUserDto user);
}