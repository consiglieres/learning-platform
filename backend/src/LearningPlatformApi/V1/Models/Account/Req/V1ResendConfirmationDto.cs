using System.ComponentModel.DataAnnotations;

namespace LearningPlatformApi.V2.Account.Req;

public class V1ResendConfirmationDto
{
    [Required][EmailAddress] public string Email { get; set; }
}