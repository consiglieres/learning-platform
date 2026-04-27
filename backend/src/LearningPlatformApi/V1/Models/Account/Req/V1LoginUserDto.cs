using System.ComponentModel.DataAnnotations;

namespace LearningPlatformApi.V1.Models.Account.Req;

public class V1LoginUserDto
{
    [Required][EmailAddress] public required string Email { get; set; }

    [Required] public required string Password { get; set; }

    public bool RememberMe { get; set; }
}