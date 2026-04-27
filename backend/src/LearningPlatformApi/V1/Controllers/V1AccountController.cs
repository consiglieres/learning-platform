using LearningPlatformApi.Mapper;
using LearningPlatformApi.Services;
using LearningPlatformApi.V1.Models.Account.Req;
using LearningPlatformApi.V2.Account.Req;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatformApi.V1.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class V1AccountController : ControllerBase
{
    private readonly IUserAuthenticationService authenticationService;
    private readonly IUserEmailService emailService;
    private readonly IUserProfileService profileService;
    private readonly IUserRegistrationService registrationService;
    private readonly IUserMapper userMapper;

    public V1AccountController(
        IUserRegistrationService registrationService,
        IUserAuthenticationService authenticationService,
        IUserProfileService profileService,
        IUserEmailService emailService,
        IUserMapper userMapper)
    {
        this.registrationService = registrationService;
        this.authenticationService = authenticationService;
        this.profileService = profileService;
        this.emailService = emailService;
        this.userMapper = userMapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] V1RegisterUserDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var registerDomain = userMapper.MapToDomain(registerDto);
        var resultState = await registrationService.RegisterUserAsync(registerDomain);

        return resultState.Match<IActionResult>(
            exists => Conflict(new ProblemDetails
            {
                Title = "Registration Failed",
                Detail = $"User with email {exists.Identification} already registered",
                Status = StatusCodes.Status409Conflict
            }),
            notSuccess => BadRequest(notSuccess.OperationInfo),
            _ => Ok(new { message = "Registration successful. Please check your email." })
        );
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            return BadRequest("Invalid email confirmation request");

        var confirmation = await emailService.ConfirmEmailAsync(email, token);

        return confirmation.Match<IActionResult>(
            notExists => NotFound(new ProblemDetails
            {
                Title = "Email confirmation failed",
                Detail = $"User with email {notExists.EntityId} not found",
                Status = StatusCodes.Status404NotFound
            }),
            notSuccess => BadRequest(new ProblemDetails
            {
                Title = "Email confirmation failed",
                Detail = "Email confirmation error occured",
                Status = StatusCodes.Status400BadRequest,
                Extensions = new Dictionary<string, object>
                {
                    ["errors"] = notSuccess.OperationInfo.Select(e => e.Description)
                }!
            }),
            _ => Ok(new { message = "Email confirmed successfully. You can now login." })
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] V1LoginUserDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await authenticationService.LoginAsync(
            loginDto.Email,
            loginDto.Password,
            loginDto.RememberMe);

        return await result.Match<Task<IActionResult>>(
            async _ => await LoginSuccessResponse(loginDto.Email),
            authError => Task.FromResult<IActionResult>(Unauthorized(new { error = authError.Message })),
            lockedError => Task.FromResult<IActionResult>(Unauthorized(new { error = lockedError.Message })),
            emailNotConfirmed =>
                Task.FromResult<IActionResult>(Unauthorized(new { error = emailNotConfirmed.Message })),
            deactivatedError => Task.FromResult<IActionResult>(Unauthorized(new { error = deactivatedError.Message }))
        );
    }

    private async Task<IActionResult> LoginSuccessResponse(string email)
    {
        var user = await profileService.GetUserByEmailAsync(email);
        var roles = await profileService.GetUserRolesAsync(user!);

        return Ok(new
        {
            message = "Logged in successfully",
            user = new
            {
                user!.Id,
                user.Email,
                user.EmailConfirmed,
                Roles = roles
            }
        });
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> LogoutAsync()
    {
        await authenticationService.LogoutAsync();
        return Ok(new { message = "Logged out successfully" });
    }

    [HttpPost("logout-all")]
    [Authorize]
    public async Task<IActionResult> LogoutAllDevicesAsync()
    {
        var user = await profileService.GetCurrentUserAsync(User);
        if (user == null)
            return NotFound();

        await authenticationService.LogoutAllDevicesAsync(user);
        return Ok(new { message = "Logged out from all devices successfully" });
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var user = await profileService.GetCurrentUserAsync(User);
        if (user == null)
            return NotFound();

        var roles = await profileService.GetUserRolesAsync(user);

        return Ok(new
        {
            user.Id,
            user.Email,
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

        var user = await profileService.GetCurrentUserAsync(User);
        if (user == null)
            return NotFound();

        var result = await profileService.ChangePasswordAsync(
            user,
            changePasswordDto.CurrentPassword,
            changePasswordDto.NewPassword);

        return result.Match<IActionResult>(
            success => Ok(new { message = success.Message }),
            failed => BadRequest(new { errors = failed.OperationInfo.Errors.Select(e => e.Description) })
        );
    }

    [HttpPost("forgot-password")]
    public Task<IActionResult> ForgotPasswordAsync([FromBody] V1ForgotPasswordDto forgotPasswordDto)
    {
        // TODO: Implement password reset service
        return Task.FromResult<IActionResult>(Ok(new { message = "If the email exists, a password reset link has been sent" }));
    }

    [HttpPost("resend-confirmation")]
    public async Task<IActionResult> ResendConfirmationEmailAsync([FromBody] V1ResendConfirmationDto dto)
    {
        var result = await emailService.SendConfirmationEmailAsync(dto.Email);

        return result.Match<IActionResult>(
            success => Ok(new { message = "Confirmation email sent" }),
            notExists => Ok(new { message = "If the email exists, a confirmation link has been sent" }),
            alreadyConfirmed => BadRequest(new { error = alreadyConfirmed.Message })
        );
    }
}