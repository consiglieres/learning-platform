using System.ComponentModel.DataAnnotations;

namespace LearningPlatformApi.V1.Models;

public class V1RefreshTokenDto
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}