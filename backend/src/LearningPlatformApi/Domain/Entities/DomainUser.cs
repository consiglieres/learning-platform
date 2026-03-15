using LearningPlatformApi.Models.Base;

namespace LearningPlatformApi.Domain.Entities;

public record DomainUser(string Id) : DomainEntity<string>(Id)
{
    public required string UserName { get; set; }

    public required string NormalizedUserName { get; init; }

    public required string Email { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string NormalizedEmail { get; set; }

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