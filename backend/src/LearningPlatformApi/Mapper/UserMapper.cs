using LearningPlatformApi.Models;
using LearningPlatformApi.Persistence.Entities;
using Riok.Mapperly.Abstractions;

namespace LearningPlatformApi.Mapper;

[Mapper]
internal partial class UserMapper
{
    public partial DomainUser MapToDomain(UserEntity user);

    public partial UserEntity MapToEntity(DomainUser user);
}