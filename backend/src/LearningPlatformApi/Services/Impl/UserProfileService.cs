using System.Security.Claims;
using LearningPlatformApi.Domain.HandleStates;
using LearningPlatformApi.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using OneOf;

namespace LearningPlatformApi.Services.Impl;

public class UserProfileService(
    UserManager<UserEntity> userManager,
    SignInManager<UserEntity> signInManager)
    : IUserProfileService
{
    public async Task<UserEntity?> GetUserByEmailAsync(string email)
    {
        return await userManager.FindByEmailAsync(email);
    }

    public async Task<UserEntity?> GetUserByIdAsync(string userId)
    {
        return await userManager.FindByIdAsync(userId);
    }

    public async Task<UserEntity?> GetCurrentUserAsync(ClaimsPrincipal user)
    {
        return await userManager.GetUserAsync(user);
    }

    public async Task<IList<string>> GetUserRolesAsync(UserEntity user)
    {
        return await userManager.GetRolesAsync(user);
    }

    public async Task<OneOf<Success, OperationNotSucceeded<IdentityResult>>>
        ChangePasswordAsync(UserEntity user, string currentPassword, string newPassword)
    {
        var result = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        if (!result.Succeeded) return new OperationNotSucceeded<IdentityResult>(result);

        await userManager.UpdateSecurityStampAsync(user);
        await signInManager.SignOutAsync();

        return new Success("Password changed successfully. Please login again.");
    }
}