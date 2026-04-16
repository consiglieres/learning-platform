using LearningPlatformApi.Domain.Entities;

namespace LearningPlatformApi.Tests.Mappers.ObjectMothers;

public class UserObjectMother
{
    public static User Create(
        string id = "user-1",
        string userName = "testuser",
        string email = "test@example.com",
        bool isActive = true,
        bool emailConfirmed = true)
    {
        var user = new User(id)
        {
            UserName = userName,
            Email = email,
            NormalizedEmail = email.ToUpperInvariant(),
            IsActive = isActive,
            EmailConfirmed = emailConfirmed,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };
        
        return user;
    }

    public static User CreateWithAudit(
        string id = "user-1",
        string userName = "testuser",
        string email = "test@example.com",
        DateTimeOffset? createdAt = null,
        DateTimeOffset? updatedAt = null)
    {
        var user = Create(id, userName, email);
        
        // Используем рефлексию для установки приватных полей аудита
        var createdAtField = typeof(User).GetField(
            "<CreatedAt>k__BackingField",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        createdAtField?.SetValue(user, createdAt ?? DateTimeOffset.UtcNow);
        
        var updatedAtField = typeof(User).GetField(
            "<UpdatedAt>k__BackingField",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        updatedAtField?.SetValue(user, updatedAt ?? DateTimeOffset.UtcNow);
        
        return user;
    }
}