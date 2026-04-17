using System.ComponentModel.DataAnnotations;

namespace LearningPlatformApi.V1.Models.Account.Req;

public class V1ResendConfirmationDto
{
    [Required] [EmailAddress] public string Email { get; set; }
}