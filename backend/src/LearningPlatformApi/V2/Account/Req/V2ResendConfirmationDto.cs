using System.ComponentModel.DataAnnotations;

namespace LearningPlatformApi.V2.Account.Req;

public class V2ResendConfirmationDto
{
    [Required][EmailAddress] public required string Email { get; set; }
}