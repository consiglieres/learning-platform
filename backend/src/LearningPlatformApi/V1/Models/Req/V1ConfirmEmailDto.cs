using System.ComponentModel.DataAnnotations;

namespace LearningPlatformApi.V1.Models;

public class V1ConfirmEmailDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Token { get; set; } = string.Empty;
}