using LearningPlatformApi.Domain.HandleStates;
using LearningPlatformApi.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using OneOf;
using Success = LearningPlatformApi.Domain.HandleStates.Success;

namespace LearningPlatformApi.Services;

public interface IUserEmailService
{
    Task<OneOf<EntityNotExists, OperationNotSucceeded<IReadOnlyCollection<IdentityError>>, Success>>
        ConfirmEmailAsync(string email, string token);

    Task<OneOf<Success, EntityNotExists, EmailAlreadyConfirmedError>>
        SendConfirmationEmailAsync(string email);

    string GenerateEmailConfirmationLink(UserEntity user, string token);
}