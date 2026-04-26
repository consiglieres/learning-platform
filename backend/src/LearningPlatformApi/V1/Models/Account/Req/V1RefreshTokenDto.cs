using System.ComponentModel.DataAnnotations;

namespace LearningPlatformApi.V2.Account.Req;

public class V1RefreshTokenDto
{
    [Required] public string RefreshToken { get; set; } = string.Empty;
}