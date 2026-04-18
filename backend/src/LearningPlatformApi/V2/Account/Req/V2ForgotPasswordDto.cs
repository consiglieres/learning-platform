using System.ComponentModel.DataAnnotations;

namespace LearningPlatformApi.V2.Account.Req;

public class V2ForgotPasswordDto
{
    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;
}