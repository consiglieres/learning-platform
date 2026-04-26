using System.ComponentModel.DataAnnotations;

namespace LearningPlatformApi.V2.Account.Req;

public class V2LoginUserDto
{
    [Required][EmailAddress] public string Email { get; set; }

    [Required] public string Password { get; set; }

    public bool RememberMe { get; set; }
}