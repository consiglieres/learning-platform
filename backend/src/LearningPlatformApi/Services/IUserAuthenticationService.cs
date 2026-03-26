using LearningPlatformApi.Domain.HandleStates;
using LearningPlatformApi.Persistence.Entities;
using OneOf;
using Success = LearningPlatformApi.Domain.HandleStates.Success;

namespace LearningPlatformApi.Services;

public interface IUserAuthenticationService
{
    Task<OneOf<Success, AuthenticationError, AccountLockedError, EmailNotConfirmedError, AccountDeactivatedError>>
        LoginAsync(string email, string password, bool rememberMe);

    Task LogoutAsync();

    Task LogoutAllDevicesAsync(UserEntity user);
}