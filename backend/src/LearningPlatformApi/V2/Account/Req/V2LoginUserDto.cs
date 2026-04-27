using System.ComponentModel.DataAnnotations;

namespace LearningPlatformApi.V2.Account.Req;

public class V2LoginUserDto
{
    [Required][EmailAddress] public required string Email { get; set; }

    [Required] public required string Password { get; set; }

    public bool RememberMe { get; set; }
}