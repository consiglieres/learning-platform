using System.Security.Claims;
using LearningPlatformApi.Domain.HandleStates;
using LearningPlatformApi.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using OneOf;
using Success = LearningPlatformApi.Domain.HandleStates.Success;

namespace LearningPlatformApi.Services;

public interface IUserProfileService
{
    Task<UserEntity?> GetUserByEmailAsync(string email);
    Task<UserEntity?> GetUserByIdAsync(string userId);
    Task<UserEntity?> GetCurrentUserAsync(ClaimsPrincipal user);
    Task<IList<string>> GetUserRolesAsync(UserEntity user);

    Task<OneOf<Success, OperationNotSucceeded<IdentityResult>>>
        ChangePasswordAsync(UserEntity user, string currentPassword, string newPassword);
}