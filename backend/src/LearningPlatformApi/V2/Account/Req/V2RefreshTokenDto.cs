using System.ComponentModel.DataAnnotations;

namespace LearningPlatformApi.V2.Account.Req;

public class V2RefreshTokenDto
{
    [Required] public string RefreshToken { get; set; } = string.Empty;
}