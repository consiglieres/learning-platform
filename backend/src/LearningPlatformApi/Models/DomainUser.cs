using LearningPlatformApi.Models.Base;

namespace LearningPlatformApi.Models;

public record DomainUser(string Id) : DomainEntity<string>(Id)
{
    public string? UserName { get; init; }

    public string? NormalizedUserName { get; init; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; init; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; init; }

    public string? PasswordHash { get; init; }

    public string? SecurityStamp { get; init; }

    public string? ConcurrencyStamp { get; init; }

    public bool TwoFactorEnabled { get; init; }

    public bool LockoutEnabled { get; init; }

    public DateTimeOffset? LockoutEnd { get; init; }

    public int AccessFailedCount { get; init; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public DateTimeOffset? LastLoginAt { get; set; }

    public bool IsActive { get; set; }
}