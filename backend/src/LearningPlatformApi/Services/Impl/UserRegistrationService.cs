using System.Text;
using LearningPlatformApi.Domain.HandleStates;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Repositories.Base;
using LearningPlatformApi.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using OneOf;

namespace LearningPlatformApi.Services.Impl;

public class UserRegistrationService(
    UserManager<UserEntity> userManager,
    IEmailService emailService,
    IOptions<EmailSettings> emailOptions,
    IUnitOfWork unitOfWork) : IUserRegistrationService
{
    private readonly EmailSettings emailSettings = emailOptions.Value;

    public async Task<OneOf<EntityAlreadyExists, OperationNotSucceeded<IdentityResult>, Success>>
        RegisterUserAsync(RegisterUser registerModel, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        var existingUser = await userManager.FindByEmailAsync(registerModel.Email);
        if (existingUser != null)
            return new EntityAlreadyExists(registerModel.Email, "User with this email already exists");

        var user = new UserEntity
        {
            Email = registerModel.Email,
            UserName = registerModel.Email,
            EmailConfirmed = false,
            IsActive = true,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        var result = await userManager.CreateAsync(user, registerModel.Password);
        if (!result.Succeeded) return new OperationNotSucceeded<IdentityResult>(result);

        await userManager.AddToRoleAsync(user, "Student");

        await SendConfirmationEmailAsync(user);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        return new Success("User registered successfully. Please check your email for confirmation.");
    }

    private async Task SendConfirmationEmailAsync(UserEntity user)
    {
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        var confirmationLink = new Uri($"{emailSettings.SendConfirmationUrl}?email={user.Email}&token={encodedToken}");

        await emailService.SendEmailAsync(
            user.Email,
            "Confirm your email",
            $@"
            <h2>Welcome to Learning Platform!</h2>
            <p>Please confirm your email by clicking the link below:</p>
            <a href={confirmationLink}>Confirm Email</a>
            <p>If you didn't create an account, you can ignore this email.</p>"
        );
    }
}