using LearningPlatformApi.Domain.HandleStates;
using LearningPlatformApi.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using OneOf;
using Success = LearningPlatformApi.Domain.HandleStates.Success;

namespace LearningPlatformApi.Services;

public interface IUserRegistrationService
{
    Task<OneOf<EntityAlreadyExists, OperationNotSucceeded<IdentityResult>, Success>> RegisterUserAsync(
        RegisterUser registerModel, CancellationToken cancellationToken = default);
}