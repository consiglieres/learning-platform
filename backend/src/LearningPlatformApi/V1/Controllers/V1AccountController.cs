using System.Text;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Services;
using LearningPlatformApi.V1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace LearningPlatformApi.V1.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class V1AccountController : ControllerBase
{
    private readonly UserManager<AppUser> userManager;
    private readonly SignInManager<AppUser> signInManager;
    private readonly IEmailService emailService;

    public V1AccountController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IEmailService emailService)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.emailService = emailService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] V1RegisterUserDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingUser = await userManager.FindByEmailAsync(registerDto.Email);
        if (existingUser != null)
        {
            return BadRequest(new { error = "User with this email already exists" });
        }

        var user = new AppUser
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            EmailConfirmed = false,
            IsActive = true
        };

        var result = await userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(new { errors = result.Errors.Select(e => e.Description) });
        }

        await userManager.AddToRoleAsync(user, "Student");

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        
        var confirmationLink = Url.Action(
            nameof(ConfirmEmail),
            "V1Account",
            new { email = user.Email, token = encodedToken },
            Request.Scheme);

        await emailService.SendEmailAsync(
            user.Email,
            "Confirm your email",
            $@"
            <h2>Welcome to Learning Platform!</h2>
            <p>Please confirm your email by clicking the link below:</p>
            <a href={confirmationLink}>Confirm Email</a>
            <p>If you didn't create an account, you can ignore this email.</p>"
        );

        return Ok(new { message = "Registration successful. Please check your email to confirm your account." });
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            return BadRequest("Invalid email confirmation request");

        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
            return BadRequest("User not found");

        var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
        var result = await userManager.ConfirmEmailAsync(user, decodedToken);

        if (!result.Succeeded)
            return BadRequest("Email confirmation failed");

        return Ok(new { message = "Email confirmed successfully. You can now login." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] V1LoginUserDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
            return Unauthorized(new { error = "Invalid email or password" });

        if (!user.EmailConfirmed)
            return Unauthorized(new { error = "Please confirm your email before logging in" });

        if (!user.IsActive)
            return Unauthorized(new { error = "Account is deactivated. Contact support." });

        var result = await signInManager.PasswordSignInAsync(
            user, 
            loginDto.Password,
            loginDto.RememberMe, // "Запомнить меня"
            lockoutOnFailure: true); // Блокировка при неудачных попытках

        if (result.IsLockedOut)
        {
            return Unauthorized(new { error = "Account locked out. Try again later." });
        }

        if (result.RequiresTwoFactor)
        {
            return Ok(new { requiresTwoFactor = true, message = "Two-factor authentication required" });
        }

        if (!result.Succeeded)
        {
            return Unauthorized(new { error = "Invalid email or password" });
        }

        user.LastLoginAt = DateTime.UtcNow;
        await userManager.UpdateAsync(user);

        var roles = await userManager.GetRolesAsync(user);

        return Ok(new
        {
            message = "Logged in successfully",
            user = new
            {
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                user.EmailConfirmed,
                Roles = roles
            }
        });
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> LogoutAsync()
    {
        await signInManager.SignOutAsync();
        
        return Ok(new { message = "Logged out successfully" });
    }

    [HttpPost("logout-all")]
    [Authorize]
    public async Task<IActionResult> LogoutAllDevicesAsync()
    {
        var user = await userManager.GetUserAsync(User);
        
        await userManager.UpdateSecurityStampAsync(user);
        
        await signInManager.SignOutAsync();
        
        return Ok(new { message = "Logged out from all devices successfully" });
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var user = await userManager.GetUserAsync(User);
        
        if (user == null)
            return NotFound();

        var roles = await userManager.GetRolesAsync(user);

        return Ok(new
        {
            user.Id,
            user.Email,
            user.FirstName,
            user.LastName,
            user.EmailConfirmed,
            user.IsActive,
            user.CreatedAt,
            user.LastLoginAt,
            Roles = roles
        });
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] V1ChangePasswordDto changePasswordDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();

        var result = await userManager.ChangePasswordAsync(
            user, 
            changePasswordDto.CurrentPassword, 
            changePasswordDto.NewPassword);

        if (!result.Succeeded)
        {
            return BadRequest(new { errors = result.Errors.Select(e => e.Description) });
        }

        // После смены пароля обновляем Security Stamp для инвалидации всех сессий
        await userManager.UpdateSecurityStampAsync(user);
        
        // Предлагаем пользователю войти заново
        await signInManager.SignOutAsync();

        return Ok(new { message = "Password changed successfully. Please login again." });
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPasswordAsync([FromBody] V1ForgotPasswordDto forgotPasswordDto)
    {
        var user = await userManager.FindByEmailAsync(forgotPasswordDto.Email);
        if (user == null || !user.EmailConfirmed)
        {
            return Ok(new { message = "If the email exists, a password reset link has been sent" });
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
    
        await emailService.SendEmailAsync(
            user.Email,
            "Reset your password",
            $@"
        <h2>Password Reset Request</h2>
        <p>Use this code to reset your password:</p>
        <p><strong>Reset Code:</strong> {encodedToken}</p>
        <p>Or click the link below (if you're using our web app):</p>
        <a href='need url'>
            Reset Password
        </a>
        <p>If you didn't request this, you can ignore this email.</p>"
        );

        return Ok(new { message = "If the email exists, a password reset link has been sent" });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] V1ResetPasswordDto resetPasswordDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);
        if (user == null)
        {
            return BadRequest(new { error = "Invalid request" });
        }

        var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordDto.Token));
        var result = await userManager.ResetPasswordAsync(user, decodedToken, resetPasswordDto.NewPassword);

        if (!result.Succeeded)
        {
            return BadRequest(new { errors = result.Errors.Select(e => e.Description) });
        }

        return Ok(new { message = "Password reset successfully. You can now login." });
    }

    [HttpPost("resend-confirmation")]
    public async Task<IActionResult> ResendConfirmationEmailAsync([FromBody] V1ResendConfirmationDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null)
        {
            return Ok(new { message = "If the email exists, a confirmation link has been sent" });
        }

        if (user.EmailConfirmed)
            return BadRequest(new { error = "Email is already confirmed" });

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        
        var confirmationLink = Url.Action(
            nameof(ConfirmEmail),
            "V1Account",
            new { email = user.Email, token = encodedToken },
            Request.Scheme);

        await emailService.SendEmailAsync(
            user.Email,
            "Confirm your email",
            $@"
            <h2>Learning Platform Email Confirmation</h2>
            <p>Please confirm your email by clicking the link below:</p>
            <a href='{confirmationLink}'>Confirm Email</a>"
        );

        return Ok(new { message = "Confirmation email sent" });
    }
}