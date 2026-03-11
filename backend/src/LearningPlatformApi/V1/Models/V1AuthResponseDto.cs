namespace LearningPlatformApi.V1.Models;

public class V1AuthResponseDto
{
    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }

    public bool EmailConfirmed { get; set; }
}