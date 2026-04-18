using LearningPlatformApi.Mapper;
using LearningPlatformApi.Services;
using LearningPlatformApi.V1.Mapper;
using LearningPlatformApi.V2.Account.Req;
using LearningPlatformApi.V2.Account.Res;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatformApi.V2.Controllers;

[Route("api/v2/accounts")]
[ApiController]
public class V2AccountController : ControllerBase
{
    private readonly IUserAuthenticationService authenticationService;
    private readonly IUserEmailService emailService;
    private readonly IUserProfileService profileService;
    private readonly IUserRegistrationService registrationService;
    private readonly IUserMapper userMapper;
    private readonly IV1ResDtoMapper v1ResDtoMapper;

    public V2AccountController(
        IUserRegistrationService registrationService,
        IUserAuthenticationService authenticationService,
        IUserProfileService profileService,
        IUserEmailService emailService,
        IUserMapper userMapper,
        IV1ResDtoMapper v1ResDtoMapper)
    {
        this.registrationService = registrationService;
        this.authenticationService = authenticationService;
        this.profileService = profileService;
        this.emailService = emailService;
        this.userMapper = userMapper;
        this.v1ResDtoMapper = v1ResDtoMapper;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync([FromBody] V2RegisterUserDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var registerDomain = userMapper.MapToDomain(registerDto);
        var resultState = await registrationService.RegisterUserAsync(registerDomain);

        return resultState.Match<ActionResult>(
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
    public async Task<IActionResult> LoginAsync([FromBody] V2LoginUserDto loginDto)
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

    private async Task<ActionResult> LoginSuccessResponse(string email)
    {
        var user = await profileService.GetUserByEmailAsync(email);
        var roles = await profileService.GetUserRolesAsync(user!);

        return Ok(v1ResDtoMapper.Map(userMapper.MapToDomain(user!)));
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
    public async Task<ActionResult<V2UserResDto>> GetCurrentUser()
    {
        var user = await profileService.GetCurrentUserAsync(User);
        if (user == null)
            return NotFound();

        var roles = await profileService.GetUserRolesAsync(user);

        return Ok(v1ResDtoMapper.Map(userMapper.MapToDomain(user)));
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] V2ChangePasswordDto changePasswordDto)
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
    public async Task<IActionResult> ForgotPasswordAsync([FromBody] V2ForgotPasswordDto forgotPasswordDto)
    {
        // TODO: Implement password reset service
        return Ok(new { message = "If the email exists, a password reset link has been sent" });
    }

    [HttpPost("resend-confirmation")]
    public async Task<IActionResult> ResendConfirmationEmailAsync([FromBody] V2ResendConfirmationDto dto)
    {
        var result = await emailService.SendConfirmationEmailAsync(dto.Email);

        return result.Match<IActionResult>(
            success => Ok(new { message = "Confirmation email sent" }),
            notExists => Ok(new { message = "If the email exists, a confirmation link has been sent" }),
            alreadyConfirmed => BadRequest(new { error = alreadyConfirmed.Message })
        );
    }
}