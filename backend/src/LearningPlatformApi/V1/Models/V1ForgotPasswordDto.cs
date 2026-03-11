using System.ComponentModel.DataAnnotations;

namespace LearningPlatformApi.V1.Models;

public class V1ForgotPasswordDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}