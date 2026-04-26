/*using System.Runtime.InteropServices.JavaScript;
using AnonymousData;
using AwesomeAssertions;
using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.Entities.Tasks;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Domain.ValueObjects.Page;
using LearningPlatformApi.Domain.ValueObjects.Task;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Mapper.Impl;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Entities.Page;
using LearningPlatformApi.Tests.Mappers.ObjectMothers;

namespace LearningPlatformApi.Tests.Mappers;

public class CodingTaskMapperTests
{
    private readonly IDbEntityMapper<Lesson, string, LessonEntity, string> lessonMapper;
    private readonly IDbEntityMapper<Page, string, PageEntity, string> pageMapper;
    private readonly IUserMapper userMapper;
    private readonly CodingTaskMapper codingTaskMapper;

    public CodingTaskMapperTests()
    {
        userMapper  = new UserMapper();
        lessonMapper = new LessonMapper();
        pageMapper = new PageMapper(userMapper);
        codingTaskMapper = new CodingTaskMapper(lessonMapper, pageMapper, userMapper);
    }

    #region Map (Entity -> Domain) Tests

    [Test]
    public void Map_WhenEntityIsValid_ShouldMapToCodingTaskCorrectly()
    {
        // Arrange
        var user = UserObjectMother.Create();
        
        var userEntity = new UserEntity
        {
            Id = Guid.NewGuid().ToString(),
            Email = "test@example.com",
            UserName = "testuser",
            NormalizedEmail = "testuser"
        };

        var lesson = new Lesson(Any.String(), Any.Int(), Any.Int(), new Module());
        var lessonEntity = new LessonEntity("lesson-1")
        {
            Name = "Test Lesson",
            Order = 1
        };

        var pageEntity = new PageEntity("page-1")
        {
            TypeCode = "task",
            TypeName = "Задание"
        };
        
        var page = Page.EmptyPage(PageType.Task);

        var entity = new CodingTaskEntity("task-1")
        {
            Name = "Coding Task 1",
            Order = 1,
            DifficultyCategory = "Medium",
            DifficultyPoints = 20,
            InitialCode = "print('Hello')",
            TestCode = "assert hello() == 'Hello'",
            LessonId = "lesson-1",
            Lesson = lessonEntity,
            PageId = "page-1",
            Page = pageEntity,
            VersionOrder = 1,
            Tag = "v1",
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-5),
            CreatedBy = "user-1",
            CreatedByUser = userEntity,
            UpdatedAt = DateTimeOffset.UtcNow.AddDays(-1),
            UpdatedBy = "user-1",
            UpdatedByUser = userEntity,
            DeletedAt = null,
            DeletedBy = null,
            DeletedByUser = null
        };

        lessonMapper.Setup(x => x.Map(lessonEntity)).Returns(lesson);
        pageMapper.Setup(x => x.Map(pageEntity)).Returns(page);
        userMapper.Setup(x => x.MapToDomain(userEntity)).Returns(user);

        // Act
        var result = codingTaskMapper.Map(entity);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("task-1");
        result.Name.Should().Be("Coding Task 1");
        result.Order.Should().Be(1);
        result.Difficulty.Name.Should().Be("Medium");
        result.Difficulty.BasePoints.Should().Be(20);
        result.InitialCode.Should().Be("print('Hello')");
        result.TestCode.Should().Be("assert hello() == 'Hello'");
        result.Lesson.Should().Be(lesson);
        result.PageContent.Should().Be(page);
        result.Version.Order.Should().Be(1);
        result.Version.Tag.Should().Be("v1");
        result.CreatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow.AddDays(-5), TimeSpan.FromSeconds(1));
        result.CreatedBy.Should().Be(user);
        result.UpdatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow.AddDays(-1), TimeSpan.FromSeconds(1));
        result.UpdatedBy.Should().Be(user);
        result.DeletedAt.Should().BeNull();
        result.DeletedBy.Should().BeNull();
    }

    [Test]
    public void Map_WhenEntityHasNoUpdatedBy_ShouldMapUpdatedByAsNull()
    {
        // Arrange
        var user = new User("user-1")
        {
            UserName = "testuser",
            Email = "test@example.com"
        };
        
        var userEntity = new UserEntity
        {
            Id = "user-1",
            UserName = "testuser",
            Email = "test@example.com"
        };

        var lesson = new Lesson("lesson-1", "Test Lesson", 1, null!);
        var lessonEntity = new LessonEntity("lesson-1")
        {
            Name = "Test Lesson",
            Order = 1
        };

        var pageEntity = new PageEntity("page-1")
        {
            TypeCode = "task",
            TypeName = "Задание"
        };
        
        var page = Page.EmptyPage(PageType.Task);

        var entity = new CodingTaskEntity("task-1")
        {
            Name = "Coding Task 1",
            Order = 1,
            DifficultyCategory = "Easy",
            DifficultyPoints = 10,
            InitialCode = "print('Hello')",
            TestCode = "assert hello() == 'Hello'",
            LessonId = "lesson-1",
            Lesson = lessonEntity,
            PageId = "page-1",
            Page = pageEntity,
            VersionOrder = 1,
            Tag = "v1",
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-5),
            CreatedBy = "user-1",
            CreatedByUser = userEntity,
            UpdatedAt = null,
            UpdatedBy = null,
            UpdatedByUser = null,
            DeletedAt = null,
            DeletedBy = null,
            DeletedByUser = null
        };

        lessonMapper.Setup(x => x.Map(lessonEntity)).Returns(lesson);
        pageMapper.Setup(x => x.Map(pageEntity)).Returns(page);
        userMapper.Setup(x => x.MapToDomain(userEntity)).Returns(user);

        // Act
        var result = codingTaskMapper.Map(entity);

        // Assert
        result.UpdatedAt.Should().BeNull();
        result.UpdatedBy.Should().BeNull();
    }

    [Test]
    public void Map_WhenEntityHasNoPage_ShouldUseEmptyPage()
    {
        // Arrange
        var user = new User("user-1")
        {
            UserName = "testuser",
            Email = "test@example.com"
        };
        
        var userEntity = new UserEntity
        {
            Id = "user-1",
            UserName = "testuser",
            Email = "test@example.com"
        };

        var lesson = new Lesson("lesson-1", "Test Lesson", 1, null!);
        var lessonEntity = new LessonEntity("lesson-1")
        {
            Name = "Test Lesson",
            Order = 1
        };

        var entity = new CodingTaskEntity("task-1")
        {
            Name = "Coding Task 1",
            Order = 1,
            DifficultyCategory = "Hard",
            DifficultyPoints = 30,
            InitialCode = "print('Hello')",
            TestCode = "assert hello() == 'Hello'",
            LessonId = "lesson-1",
            Lesson = lessonEntity,
            Page = null,
            VersionOrder = 1,
            Tag = "v1",
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-5),
            CreatedBy = "user-1",
            CreatedByUser = userEntity,
            UpdatedAt = null,
            UpdatedBy = null,
            UpdatedByUser = null,
            DeletedAt = null,
            DeletedBy = null,
            DeletedByUser = null
        };

        lessonMapper.Setup(x => x.Map(lessonEntity)).Returns(lesson);
        userMapper.Setup(x => x.MapToDomain(userEntity)).Returns(user);

        // Act
        var result = codingTaskMapper.Map(entity);

        // Assert
        result.PageContent.Should().NotBeNull();
        result.PageContent.Type.Should().Be(PageType.Task);
        result.PageContent.Id.Should().NotBeNullOrEmpty();
    }

    #endregion

    #region Map (Domain -> Entity) Tests

    [Test]
    public void Map_WhenDomainIsValid_ShouldMapToCodingTaskEntityCorrectly()
    {
        // Arrange
        var user = new User("user-1")
        {
            UserName = "testuser",
            Email = "test@example.com"
        };
        
        var userEntity = new UserEntity
        {
            Id = "user-1",
            UserName = "testuser",
            Email = "test@example.com"
        };

        var module = new Module("module-1", "Test Module", 1, null!);
        var course = new Course("course-1", "Test Course", user);
        var lesson = new Lesson("lesson-1", "Test Lesson", 1, module);
        var page = Page.EmptyPage(PageType.Task);
        var pageEntity = new PageEntity("page-1")
        {
            TypeCode = "task",
            TypeName = "Задание"
        };

        var domain = new CodingTask(
            "Coding Task 1",
            1,
            Difficulty.Medium,
            lesson,
            page,
            "print('Hello')",
            "assert hello() == 'Hello'")
        {
            Id = "task-1"
        };

        // Устанавливаем аудит через рефлексию для теста
        var createdAtField = typeof(CodingTask).GetField(
            "<CreatedAt>k__BackingField",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        createdAtField?.SetValue(domain, DateTimeOffset.UtcNow.AddDays(-5));
        
        var createdByField = typeof(CodingTask).GetField(
            "<CreatedBy>k__BackingField",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        createdByField?.SetValue(domain, user);

        var versionField = typeof(CodingTask).GetField(
            "<Version>k__BackingField",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        versionField?.SetValue(domain, new EntityVersion(1, "v1"));

        lessonMapper.Setup(x => x.Map(lesson)).Returns(new LessonEntity("lesson-1"));
        pageMapper.Setup(x => x.Map(page)).Returns(pageEntity);
        userMapper.Setup(x => x.MapToEntity(user)).Returns(userEntity);

        // Act
        var result = codingTaskMapper.Map(domain);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("task-1");
        result.Name.Should().Be("Coding Task 1");
        result.Order.Should().Be(1);
        result.DifficultyCategory.Should().Be("Medium");
        result.DifficultyPoints.Should().Be(20);
        result.InitialCode.Should().Be("print('Hello')");
        result.TestCode.Should().Be("assert hello() == 'Hello'");
        result.LessonId.Should().Be("lesson-1");
        result.PageId.Should().Be("page-1");
        result.VersionOrder.Should().Be(1);
        result.Tag.Should().Be("v1");
        result.CreatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow.AddDays(-5), TimeSpan.FromSeconds(1));
        result.CreatedBy.Should().Be("user-1");
        result.CreatedByUser.Should().Be(userEntity);
    }

    [Test]
    public void Map_WhenDomainHasNoUpdatedBy_ShouldMapUpdatedByAsNull()
    {
        // Arrange
        var user = new User("user-1")
        {
            UserName = "testuser",
            Email = "test@example.com"
        };
        
        var userEntity = new UserEntity
        {
            Id = "user-1",
            UserName = "testuser",
            Email = "test@example.com"
        };

        var module = new Module("module-1", "Test Module", 1, null!);
        var lesson = new Lesson("lesson-1", "Test Lesson", 1, module);
        var page = Page.EmptyPage(PageType.Task);
        var pageEntity = new PageEntity("page-1")
        {
            TypeCode = "task",
            TypeName = "Задание"
        };

        var domain = new CodingTask(
            "Coding Task 1",
            1,
            Difficulty.Easy,
            lesson,
            page,
            "print('Hello')",
            "assert hello() == 'Hello'")
        {
            Id = "task-1"
        };

        var createdAtField = typeof(CodingTask).GetField(
            "<CreatedAt>k__BackingField",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        createdAtField?.SetValue(domain, DateTimeOffset.UtcNow.AddDays(-5));
        
        var createdByField = typeof(CodingTask).GetField(
            "<CreatedBy>k__BackingField",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        createdByField?.SetValue(domain, user);

        lessonMapper.Setup(x => x.Map(lesson)).Returns(new LessonEntity("lesson-1"));
        pageMapper.Setup(x => x.Map(page)).Returns(pageEntity);
        userMapper.Setup(x => x.MapToEntity(user)).Returns(userEntity);

        // Act
        var result = codingTaskMapper.Map(domain);

        // Assert
        result.UpdatedAt.Should().BeNull();
        result.UpdatedBy.Should().BeNull();
        result.UpdatedByUser.Should().BeNull();
    }

    #endregion

    #region Edge Cases Tests

    [Test]
    public void Map_WhenEntityIsNull_ShouldThrowNullReferenceException()
    {
        // Act & Assert
        var act = () => codingTaskMapper.Map((CodingTaskEntity)null!);
        act.Should().Throw<NullReferenceException>();
    }

    [Test]
    public void Map_WhenDomainIsNull_ShouldThrowNullReferenceException()
    {
        // Act & Assert
        var act = () => codingTaskMapper.Map((CodingTask)null!);
        act.Should().Throw<NullReferenceException>();
    }

    [Test]
    public void MapId_ShouldReturnSameId()
    {
        // Arrange
        var id = "task-123";

        // Act
        var result = codingTaskMapper.MapId(id);

        // Assert
        result.Should().Be(id);
    }

    #endregion

    #region Integration Tests

    [Test]
    public void Map_ShouldBeReversible_DomainToEntityToDomain()
    {
        // Arrange
        var user = new User("user-1")
        {
            UserName = "testuser",
            Email = "test@example.com"
        };
        
        var userEntity = new UserEntity
        {
            Id = "user-1",
            UserName = "testuser",
            Email = "test@example.com"
        };

        var module = new Module("module-1", "Test Module", 1, null!);
        var course = new Course("course-1", "Test Course", user);
        var lesson = new Lesson("lesson-1", "Test Lesson", 1, module);
        var page = Page.EmptyPage(PageType.Task);
        var pageEntity = new PageEntity("page-1")
        {
            TypeCode = "task",
            TypeName = "Задание"
        };

        var originalDomain = new CodingTask(
            "Coding Task 1",
            1,
            Difficulty.Medium,
            lesson,
            page,
            "print('Hello')",
            "assert hello() == 'Hello'")
        {
            Id = "task-1"
        };

        lessonMapper.Setup(x => x.Map(It.IsAny<Lesson>())).Returns(new LessonEntity("lesson-1"));
        lessonMapper.Setup(x => x.Map(It.IsAny<LessonEntity>())).Returns(lesson);
        pageMapper.Setup(x => x.Map(It.IsAny<Page>())).Returns(pageEntity);
        pageMapper.Setup(x => x.Map(It.IsAny<PageEntity>())).Returns(page);
        userMapper.Setup(x => x.MapToDomain(It.IsAny<UserEntity>())).Returns(user);
        userMapper.Setup(x => x.MapToEntity(It.IsAny<User>())).Returns(userEntity);

        // Act
        var entity = codingTaskMapper.Map(originalDomain);
        var resultDomain = codingTaskMapper.Map(entity);

        // Assert
        resultDomain.Id.Should().Be(originalDomain.Id);
        resultDomain.Name.Should().Be(originalDomain.Name);
        resultDomain.Order.Should().Be(originalDomain.Order);
        resultDomain.Difficulty.Name.Should().Be(originalDomain.Difficulty.Name);
        resultDomain.InitialCode.Should().Be(originalDomain.InitialCode);
        resultDomain.TestCode.Should().Be(originalDomain.TestCode);
    }

    #endregion
}*/