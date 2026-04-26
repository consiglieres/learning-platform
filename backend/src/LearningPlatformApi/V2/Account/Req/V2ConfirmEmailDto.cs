using System.ComponentModel.DataAnnotations;

namespace LearningPlatformApi.V2.Account.Req;

public class V2ConfirmEmailDto
{
    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;

    [Required] public string Token { get; set; } = string.Empty;
}