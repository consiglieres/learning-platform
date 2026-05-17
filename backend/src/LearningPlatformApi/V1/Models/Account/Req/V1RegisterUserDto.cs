using System.ComponentModel.DataAnnotations;

namespace LearningPlatformApi.V1.Models.Account.Req;

public class V1RegisterUserDto
{
    [Required] public string FullName { get; set; } = string.Empty;
    
    [Required][EmailAddress] public string Email { get; set; } = string.Empty;

    [Required][MinLength(6)] public string Password { get; set; } = string.Empty;

    [Required][Compare(nameof(Password))] public string ConfirmPassword { get; set; } = string.Empty;
}