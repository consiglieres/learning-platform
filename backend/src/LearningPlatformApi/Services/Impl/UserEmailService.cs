using OneOf;
using System.Text;
using LearningPlatformApi.Domain.HandleStates;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace LearningPlatformApi.Services.Impl;

public class UserEmailService(
    UserManager<UserEntity> userManager,
    IEmailService emailService,
    IOptions<EmailSettings> emailOptions)
    : IUserEmailService
{

    public async Task<OneOf<EntityNotExists, OperationNotSucceeded<IReadOnlyCollection<IdentityError>>, Success>>
        ConfirmEmailAsync(string email, string token)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
            return new EntityNotExists(email, "User not found");

        if (user.EmailConfirmed)
        {
            return new Success();
        }

        var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
        var result = await userManager.ConfirmEmailAsync(user, decodedToken);

        if (!result.Succeeded)
            return new OperationNotSucceeded<IReadOnlyCollection<IdentityError>>(result.Errors.ToList());

        return new Success();
    }

    public async Task<OneOf<Success, EntityNotExists, EmailAlreadyConfirmedError>>
        SendConfirmationEmailAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return new EntityNotExists(email, "User not found");
        }

        if (user.EmailConfirmed)
        {
            return new EmailAlreadyConfirmedError("Email is already confirmed");
        }

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = GenerateEmailConfirmationLink(user, token);

        await emailService.SendEmailAsync(
            user.Email,
            "Confirm your email",
            $@"
            <h2>Learning Platform Email Confirmation</h2>
            <p>Please confirm your email by clicking the link below:</p>
            <a href='{confirmationLink}'>Confirm Email</a>"
        );

        return new Success();
    }

    public string GenerateEmailConfirmationLink(UserEntity user, string token)
    {
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        return $"{emailOptions.Value.SendConfirmationUrl}?email={user.Email}&token={encodedToken}";
    }
}