using LearningPlatformApi.Domain.HandleStates;
using LearningPlatformApi.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using OneOf;

namespace LearningPlatformApi.Services.Impl;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly SignInManager<UserEntity> signInManager;
    private readonly UserManager<UserEntity> userManager;

    public UserAuthenticationService(
        UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    public async Task<OneOf<Success, AuthenticationError, AccountLockedError, EmailNotConfirmedError,
            AccountDeactivatedError>>
        LoginAsync(string email, string password, bool rememberMe)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null) return new AuthenticationError("Invalid email or password");

        if (!user.EmailConfirmed) return new EmailNotConfirmedError("Please confirm your email before logging in");

        if (!user.IsActive) return new AccountDeactivatedError("Account is deactivated. Contact support.");

        var result = await signInManager.PasswordSignInAsync(
            user,
            password,
            rememberMe,
            true);

        if (result.IsLockedOut) return new AccountLockedError("Account locked out. Try again later.");

        if (!result.Succeeded) return new AuthenticationError("Invalid email or password");

        user.LastLoginAt = DateTimeOffset.UtcNow;
        await userManager.UpdateAsync(user);

        return new Success();
    }

    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }

    public async Task LogoutAllDevicesAsync(UserEntity user)
    {
        await userManager.UpdateSecurityStampAsync(user);
        await signInManager.SignOutAsync();
    }
}