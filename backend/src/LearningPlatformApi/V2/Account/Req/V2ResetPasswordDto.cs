using System.ComponentModel.DataAnnotations;

namespace LearningPlatformApi.V2.Account.Req;

public class V2ResetPasswordDto
{
    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;

    [Required] public string Token { get; set; } = string.Empty;

    [Required] [MinLength(6)] public string NewPassword { get; set; } = string.Empty;
}