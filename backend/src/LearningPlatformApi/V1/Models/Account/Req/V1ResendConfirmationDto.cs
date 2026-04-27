using System.ComponentModel.DataAnnotations;

namespace LearningPlatformApi.V1.Models.Account.Req;

public class V1ResendConfirmationDto
{
    [Required][EmailAddress] public required string Email { get; set; }
}