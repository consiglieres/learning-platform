using AwesomeAssertions;
using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Mapper.Impl;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.V2.Account.Req;

namespace LearningPlatformApi.Tests.Mappers;

public class UserMapperTests
{
    private readonly UserMapper mapper = new();

    #region MapToDomain Tests (UserEntity -> User)

    [Test]
    public void MapToDomain_WhenUserEntityIsValid_ShouldMapToUserCorrectly()
    {
        // Arrange
        var entity = new UserEntity
        {
            Id = "user-123",
            FullName = "testuser",
            Email = "test@example.com",
            NormalizedEmail = "TEST@EXAMPLE.COM",
            EmailConfirmed = true,
            PasswordHash = "hashed_password",
            SecurityStamp = "stamp123",
            ConcurrencyStamp = "concurrency123",
            TwoFactorEnabled = false,
            LockoutEnabled = true,
            LockoutEnd = DateTimeOffset.UtcNow.AddHours(1),
            AccessFailedCount = 0,
            LastLoginAt = DateTimeOffset.UtcNow,
            IsActive = true,
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-10),
            UpdatedAt = DateTimeOffset.UtcNow.AddDays(-1),
            PhoneNumber = "1234567890",
            PhoneNumberConfirmed = true
        };

        // Act
        var result = mapper.MapToDomain(entity);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("user-123");
        result.FullName.Should().Be("testuser");
        result.Email.Should().Be("test@example.com");
        result.NormalizedEmail.Should().Be("TEST@EXAMPLE.COM");
        result.EmailConfirmed.Should().BeTrue();
        result.PasswordHash.Should().Be("hashed_password");
        result.SecurityStamp.Should().Be("stamp123");
        result.ConcurrencyStamp.Should().Be("concurrency123");
        result.TwoFactorEnabled.Should().BeFalse();
        result.LockoutEnabled.Should().BeTrue();
        result.LockoutEnd.Should().NotBeNull();
        result.AccessFailedCount.Should().Be(0);
        result.LastLoginAt.Should().NotBeNull();
        result.IsActive.Should().BeTrue();
        result.CreatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow.AddDays(-10), TimeSpan.FromSeconds(1));
        result.UpdatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow.AddDays(-1), TimeSpan.FromSeconds(1));
    }

    [Test]
    public void MapToDomain_WhenUserEntityHasNullLockoutEnd_ShouldMapLockoutEndAsNull()
    {
        // Arrange
        var entity = new UserEntity
        {
            Id = "user-456",
            FullName = "testuser2",
            Email = "test2@example.com",
            NormalizedEmail = "TEST2@EXAMPLE.COM",
            LockoutEnd = null
        };

        // Act
        var result = mapper.MapToDomain(entity);

        // Assert
        result.LockoutEnd.Should().BeNull();
    }

    [Test]
    public void MapToDomain_WhenUserEntityHasNullLastLoginAt_ShouldMapLastLoginAtAsNull()
    {
        // Arrange
        var entity = new UserEntity
        {
            Id = "user-789",
            FullName = "testuser3",
            Email = "test3@example.com",
            NormalizedEmail = "TEST3@EXAMPLE.COM",
            LastLoginAt = null
        };

        // Act
        var result = mapper.MapToDomain(entity);

        // Assert
        result.LastLoginAt.Should().BeNull();
    }

    [Test]
    public void MapToDomain_WhenUserEntityIsMinimal_ShouldMapCorrectly()
    {
        // Arrange
        var entity = new UserEntity
        {
            Id = "user-min",
            FullName = "minimal",
            Email = "minimal@example.com",
            NormalizedEmail = "MINIMAL@EXAMPLE.COM"
        };

        // Act
        var result = mapper.MapToDomain(entity);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("user-min");
        result.FullName.Should().Be("minimal");
        result.Email.Should().Be("minimal@example.com");
        result.NormalizedEmail.Should().Be("MINIMAL@EXAMPLE.COM");
        
        // Default values
        result.EmailConfirmed.Should().BeFalse();
        result.TwoFactorEnabled.Should().BeFalse();
        result.LockoutEnabled.Should().BeFalse();
        result.AccessFailedCount.Should().Be(0);
        result.IsActive.Should().BeTrue();
    }

    #endregion

    #region MapToEntity Tests (User -> UserEntity)

    [Test]
    public void MapToEntity_WhenUserIsValid_ShouldMapToUserEntityCorrectly()
    {
        // Arrange
        var user = new User("user-123")
        {
            FullName = "testuser",
            Email = "test@example.com",
            NormalizedEmail = "TEST@EXAMPLE.COM",
            EmailConfirmed = true,
            PasswordHash = "hashed_password",
            SecurityStamp = "stamp123",
            ConcurrencyStamp = "concurrency123",
            TwoFactorEnabled = false,
            LockoutEnabled = true,
            LockoutEnd = DateTimeOffset.UtcNow.AddHours(1),
            AccessFailedCount = 0,
            LastLoginAt = DateTimeOffset.UtcNow,
            IsActive = true,
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-10),
            UpdatedAt = DateTimeOffset.UtcNow.AddDays(-1)
        };

        // Act
        var result = mapper.MapToEntity(user);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("user-123");
        result.FullName.Should().Be("testuser");
        result.Email.Should().Be("test@example.com");
        result.NormalizedEmail.Should().Be("TEST@EXAMPLE.COM");
        result.EmailConfirmed.Should().BeTrue();
        result.PasswordHash.Should().Be("hashed_password");
        result.SecurityStamp.Should().Be("stamp123");
        result.ConcurrencyStamp.Should().Be("concurrency123");
        result.TwoFactorEnabled.Should().BeFalse();
        result.LockoutEnabled.Should().BeTrue();
        result.LockoutEnd.Should().NotBeNull();
        result.AccessFailedCount.Should().Be(0);
        result.LastLoginAt.Should().NotBeNull();
        result.IsActive.Should().BeTrue();
        result.CreatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow.AddDays(-10), TimeSpan.FromSeconds(1));
        result.UpdatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow.AddDays(-1), TimeSpan.FromSeconds(1));
    }

    [Test]
    public void MapToEntity_WhenUserHasNullLockoutEnd_ShouldMapLockoutEndAsNull()
    {
        // Arrange
        var user = new User("user-456")
        {
            FullName = "testuser2",
            Email = "test2@example.com",
            NormalizedEmail = "TEST2@EXAMPLE.COM",
            LockoutEnd = null
        };

        // Act
        var result = mapper.MapToEntity(user);

        // Assert
        result.LockoutEnd.Should().BeNull();
    }

    [Test]
    public void MapToEntity_WhenUserHasNullLastLoginAt_ShouldMapLastLoginAtAsNull()
    {
        // Arrange
        var user = new User("user-789")
        {
            FullName = "testuser3",
            Email = "test3@example.com",
            NormalizedEmail = "TEST3@EXAMPLE.COM",
            LastLoginAt = null
        };

        // Act
        var result = mapper.MapToEntity(user);

        // Assert
        result.LastLoginAt.Should().BeNull();
    }

    [Test]
    public void MapToEntity_WhenUserIsMinimal_ShouldMapCorrectly()
    {
        // Arrange
        var user = new User("user-min")
        {
            FullName = "minimal",
            Email = "minimal@example.com",
            NormalizedEmail = "MINIMAL@EXAMPLE.COM"
        };

        // Act
        var result = mapper.MapToEntity(user);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("user-min");
        result.FullName.Should().Be("minimal");
        result.Email.Should().Be("minimal@example.com");
        result.NormalizedEmail.Should().Be("MINIMAL@EXAMPLE.COM");
        
        // Игнорируемые поля (не должны маппиться)
        result.PhoneNumber.Should().BeNull();
        result.PhoneNumberConfirmed.Should().BeFalse();
        result.UserResources.Should().BeEmpty();
    }

    #endregion

    #region MapToDomain Tests (V1RegisterUserDto -> RegisterUser)

    [Test]
    public void MapToDomain_WhenV1RegisterUserDtoIsValid_ShouldMapToRegisterUserCorrectly()
    {
        // Arrange
        var dto = new V2RegisterUserDto
        {
            Email = "test@example.com",
            Password = "Password123!",
        };

        // Act
        var result = mapper.MapToDomain(dto);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be("test@example.com");
        result.Password.Should().Be("Password123!");
    }

    [Test]
    public void MapToDomain_WhenV1RegisterUserDtoHasMinimalFields_ShouldMapCorrectly()
    {
        // Arrange
        var dto = new V2RegisterUserDto
        {
            Email = "minimal@example.com",
            Password = "pass123",
        };

        // Act
        var result = mapper.MapToDomain(dto);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be("minimal@example.com");
        result.Password.Should().Be("pass123");
    }

    #endregion

    #region Edge Cases Tests

    [Test]
    public void MapToDomain_WhenUserEntityIsNull_ShouldThrowNullReferenceException()
    {
        // Arrange
        UserEntity? entity = null;

        // Act & Assert
        var act = () => mapper.MapToDomain(entity!);
        act.Should().Throw<NullReferenceException>();
    }

    [Test]
    public void MapToEntity_WhenUserIsNull_ShouldThrowNullReferenceException()
    {
        // Arrange
        User? user = null;

        // Act & Assert
        var act = () => mapper.MapToEntity(user!);
        act.Should().Throw<NullReferenceException>();
    }

    [Test]
    public void MapToDomain_WhenV1RegisterUserDtoIsNull_ShouldThrowNullReferenceException()
    {
        // Arrange
        V2RegisterUserDto? dto = null;

        // Act & Assert
        var act = () => mapper.MapToDomain(dto!);
        act.Should().Throw<NullReferenceException>();
    }

    #endregion

    #region Integration Tests

    [Test]
    public void MapToDomainAndBack_ShouldPreserveUserData()
    {
        // Arrange
        var originalEntity = new UserEntity
        {
            Id = "user-123",
            FullName = "testuser",
            Email = "test@example.com",
            NormalizedEmail = "TEST@EXAMPLE.COM",
            EmailConfirmed = true,
            PasswordHash = "hashed",
            SecurityStamp = "stamp",
            ConcurrencyStamp = "concurrency",
            TwoFactorEnabled = false,
            LockoutEnabled = true,
            LockoutEnd = DateTimeOffset.UtcNow.AddHours(1),
            AccessFailedCount = 0,
            LastLoginAt = DateTimeOffset.UtcNow,
            IsActive = true,
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-10),
            UpdatedAt = DateTimeOffset.UtcNow.AddDays(-1)
        };

        // Act
        var user = mapper.MapToDomain(originalEntity);
        var resultEntity = mapper.MapToEntity(user);

        // Assert
        resultEntity.Should().BeEquivalentTo(originalEntity, options => options
            .Excluding(e => e.PhoneNumber)
            .Excluding(e => e.PhoneNumberConfirmed)
            .Excluding(e => e.UserResources)
            .Excluding(e => e.NormalizedUserName));
    }

    [Test]
    public void MapToDomainAndBack_ShouldPreserveMinimalUserData()
    {
        // Arrange
        var originalUser = new User("user-456")
        {
            FullName = "minimal",
            Email = "minimal@example.com",
            NormalizedEmail = "MINIMAL@EXAMPLE.COM"
        };

        // Act
        var entity = mapper.MapToEntity(originalUser);
        var resultUser = mapper.MapToDomain(entity);

        // Assert
        resultUser.Should().BeEquivalentTo(originalUser);
    }

    #endregion
}