namespace LearningPlatformApi.Domain.ValueObjects;

public class RegisterUser
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}