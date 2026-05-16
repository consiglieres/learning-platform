using AwesomeAssertions;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Domain.ValueObjects.Page;
using LearningPlatformApi.Mapper.Impl;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Entities.Page;
using LearningPlatformApi.Tests.Mappers.ObjectMothers;

namespace LearningPlatformApi.Tests.Mappers;

public class PageMapperTests
{
    private readonly PageMapper mapper = new(new UserMapper());

    #region Page Mapping Tests

    [Test]
    public void Map_WhenPageEntityIsValid_ShouldMapToPageCorrectly()
    {
        // Arrange
        var entity = new PageEntity("page-123")
        {
            Order = 1,
            TypeCode = "theory",
            TypeName = "Теория",
            ContentBlocks = new List<ContentBlockEntity>(),
            CreatedByUser = new UserEntity()
            {
                Email = "test@example.com",
                FullName = "testuser",
                NormalizedEmail = "testuser"
            }
        };

        // Act
        var result = mapper.Map(entity);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("page-123");
        result.Order.Should().Be(1);
        result.Type.Code.Should().Be("theory");
        result.Type.Name.Should().Be("Теория");
        result.ContentBlocks.Should().BeEmpty();
    }
    
    [Test]
    public void Map_WhenPageEntityIsValid_ShouldMapToPageCorrectly_with_a_lot_properties()
    {
        // Arrange
        var actor = new UserEntity()
        {
            Id = Guid.NewGuid().ToString(),
            Email = "test@example.com",
            FullName = "testuser",
            NormalizedEmail = "testuser"
        };
        
        var entity = new PageEntity("page-123")
        {
            Order = 1,
            TypeCode = "theory",
            TypeName = "Теория",
            ContentBlocks = new List<ContentBlockEntity>(),
            CreatedByUser = actor,
            CreatedBy = actor.Id,
            UpdatedByUser = actor,
            UpdatedBy = actor.Id,
            UpdatedAt = DateTimeOffset.UtcNow,
            DeletedBy = actor.Id,
            DeletedByUser = actor,
            DeletedAt = DateTimeOffset.UtcNow,
        };

        // Act
        var result = mapper.Map(entity);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("page-123");
        result.Order.Should().Be(1);
        result.Type.Code.Should().Be("theory");
        result.Type.Name.Should().Be("Теория");
        result.ContentBlocks.Should().BeEmpty();
        result.CreatedBy.Id.Should().Be(actor.Id);
        result.UpdatedBy!.Id.Should().Be(actor.Id);
        result.DeletedBy!.Id.Should().Be(actor.Id);
    }


    [Test]
    public void Map_WhenPageEntityHasIntroductionType_ShouldMapToIntroductionPageType()
    {
        // Arrange
        var entity = new PageEntity("page-456")
        {
            Order = 0,
            TypeCode = "intro",
            TypeName = "Введение",
            ContentBlocks = new List<ContentBlockEntity>()
        };

        // Act
        var result = mapper.Map(entity);

        // Assert
        result.Should().NotBeNull();
        result.Type.Should().Be(PageType.Introduction);
        result.Type.Code.Should().Be("intro");
        result.Type.Name.Should().Be("Введение");
    }

    [Test]
    public void Map_WhenPageEntityHasTheoryType_ShouldMapToTheoryPageType()
    {
        // Arrange
        var entity = new PageEntity("page-789")
        {
            TypeCode = "theory",
            TypeName = "Теория",
            ContentBlocks = new List<ContentBlockEntity>()
        };

        // Act
        var result = mapper.Map(entity);

        // Assert
        result.Type.Should().Be(PageType.Theory);
    }

    [Test]
    public void Map_WhenPageEntityHasTaskType_ShouldMapToTaskPageType()
    {
        // Arrange
        var entity = new PageEntity("page-101")
        {
            TypeCode = "task",
            TypeName = "Задание",
            ContentBlocks = new List<ContentBlockEntity>()
        };

        // Act
        var result = mapper.Map(entity);

        // Assert
        result.Type.Should().Be(PageType.Task);
    }

    [Test]
    public void Map_WhenPageEntityHasUnknownType_ShouldCreateCustomPageType()
    {
        // Arrange
        var entity = new PageEntity("page-202")
        {
            TypeCode = "custom",
            TypeName = "Пользовательский тип",
            ContentBlocks = new List<ContentBlockEntity>()
        };

        // Act
        var result = mapper.Map(entity);

        // Assert
        result.Type.Should().NotBe(PageType.Introduction);
        result.Type.Should().NotBe(PageType.Theory);
        result.Type.Should().NotBe(PageType.Task);
        result.Type.Code.Should().Be("custom");
        result.Type.Name.Should().Be("Пользовательский тип");
    }

    [Test]
    public void Map_WhenPageIsValid_ShouldMapToPageEntityCorrectly()
    {
        // Arrange
        var page = new Page("page-123", 1, PageType.Theory)
        {
            ContentBlocks = new List<PageContentBlock>(),
            CreatedBy = UserObjectMother.Create()
        };

        // Act
        var result = mapper.Map(page);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("page-123");
        result.Order.Should().Be(1);
        result.TypeCode.Should().Be("theory");
        result.TypeName.Should().Be("Теория");
        result.ContentBlocks.Should().BeEmpty();
    }

    #endregion

    #region ContentBlock Mapping Tests

    [Test]
    public void Map_WhenContentBlockEntityIsValid_ShouldMapToPageContentBlockCorrectly()
    {
        // Arrange
        var entity = new ContentBlockEntity("123")
        {
            PageId = "page-1",
            Order = 1,
            Data = "<p>Hello</p>",
            Type = ContentBlockType.CreateMarkup,
            CreatedByUser = new UserEntity()
            {
                Email = "test@example.com",
                FullName = "testuser",
                NormalizedEmail = "testuser"
            }
        };

        // Act
        var result = mapper.Map(entity);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("123");
        result.PageId.Should().Be("page-1");
        result.Order.Should().Be(1);
        result.Data.Should().Be("<p>Hello</p>");
        result.Type.Should().Be(ContentBlockType.CreateMarkup);
    }

    [Test]
    public void Map_WhenPageContentBlockIsValid_ShouldMapToContentBlockEntityCorrectly()
    {
        // Arrange
        var contentBlock = new PageContentBlock(
            "123",
            "page-1",
            1,
            ContentBlockType.CreateMarkup,
            "<p>Hello</p>"
        )
        {
            CreatedBy = UserObjectMother.Create()
        };

        // Act
        var result = mapper.Map(contentBlock);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("123");
        result.PageId.Should().Be("page-1");
        result.Order.Should().Be(1);
        result.Data.Should().Be("<p>Hello</p>");
        result.Type.Should().Be(ContentBlockType.CreateMarkup);
    }

    [Test]
    public void Map_WhenContentBlockEntityHasImageType_ShouldMapToImageContentBlockType()
    {
        // Arrange
        var entity = new ContentBlockEntity("456")
        {
            Type = ContentBlockType.CreateImage,
            Data = "https://example.com/image.png",
            CreatedByUser = new UserEntity()
            {
                Email = "test@example.com",
                FullName = "testuser",
                NormalizedEmail = "testuser"
            }
        };

        // Act
        var result = mapper.Map(entity);

        // Assert
        result.Type.Should().Be(ContentBlockType.CreateImage);
    }

    [Test]
    public void Map_WhenContentBlockEntityHasVideoType_ShouldMapToVideoContentBlockType()
    {
        // Arrange
        var entity = new ContentBlockEntity("789")
        {
            Type = ContentBlockType.CreateUrl,
            Data = "https://youtube.com/watch?v=123",
            CreatedByUser = new UserEntity()
            {
                Email = "test@example.com",
                FullName = "testuser",
                NormalizedEmail = "testuser"
            }
        };

        // Act
        var result = mapper.Map(entity);

        // Assert
        result.Type.Should().Be(ContentBlockType.CreateUrl);
    }

    [Test]
    public void Map_WhenContentBlockEntityHasCodeType_ShouldMapToCodeContentBlockType()
    {
        // Arrange
        var entity = new ContentBlockEntity("101")
        {
            Type = ContentBlockType.CreateCode,
            Data = "print('Hello')",
            CreatedByUser = new UserEntity()
            {
                Email = "test@example.com",
                FullName = "testuser",
                NormalizedEmail = "testuser"
            }
        };

        // Act
        var result = mapper.Map(entity);

        // Assert
        result.Type.Should().Be(ContentBlockType.CreateCode);
    }

    #endregion

    #region Edge Cases Tests

    [Test]
    public void Map_WhenPageEntityIsNull_ShouldReturnNull()
    {
        // Act
        var result = mapper.Map((PageEntity)null!);

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public void Map_WhenPageEntityHasEmptyContentBlocks_ShouldMapToEmptyCollection()
    {
        // Arrange
        var entity = new PageEntity("page-1")
        {
            TypeCode = "theory",
            TypeName = "Теория",
            ContentBlocks = new List<ContentBlockEntity>()
        };

        // Act
        var result = mapper.Map(entity);

        // Assert
        result.ContentBlocks.Should().NotBeNull();
        result.ContentBlocks.Should().BeEmpty();
    }

    [Test]
    public void Map_WhenPageHasEmptyContentBlocks_ShouldMapToEmptyCollection()
    {
        // Arrange
        var page = new Page("page-1", 1, PageType.Theory)
        {
            ContentBlocks = new List<PageContentBlock>(),
            CreatedBy = UserObjectMother.Create()
        };

        // Act
        var result = mapper.Map(page);

        // Assert
        result.ContentBlocks.Should().NotBeNull();
        result.ContentBlocks.Should().BeEmpty();
    }

    [Test]
    public void Map_WhenPageEntityHasNullContentBlocks_ShouldMapToEmptyCollection()
    {
        // Arrange
        var entity = new PageEntity("page-1")
        {
            TypeCode = "theory",
            TypeName = "Теория",
            ContentBlocks = null!
        };

        // Act
        var result = mapper.Map(entity);

        // Assert
        result.ContentBlocks.Should().NotBeNull();
        result.ContentBlocks.Should().BeEmpty();
    }

    #endregion

    #region Integration Tests

    [Test]
    public void Map_ShouldBeReversible_PageToEntityToPage()
    {
        // Arrange
        var originalPage = new Page("121", 2, PageType.Theory)
        {
            Order = 2,
            Type = PageType.Theory,
            ContentBlocks = new List<PageContentBlock>
            {
                new("1", "page-123", 1, ContentBlockType.CreateMarkup, "<p>Test</p>")
                {
                    CreatedBy = UserObjectMother.Create("user1")
                }
            },
            CreatedBy = UserObjectMother.Create("user1")
        };

        // Act
        var entity = mapper.Map(originalPage);
        var resultPage = mapper.Map(entity);

        // Assert
        resultPage.Should().BeEquivalentTo(originalPage, options => options
            .ComparingByMembers<Page>()
            .ComparingByMembers<PageContentBlock>());
    }

    [Test]
    public void Map_ShouldBeReversible_EntityToPageToEntity()
    {
        // Arrange
        var creator = new UserEntity()
        {
            Id = Guid.NewGuid().ToString(),
            Email = "test@example.com",
            FullName = "testuser",
            NormalizedEmail = "testuser"
        };
        
        var originalEntity = new PageEntity("page-123")
        {
            Order = 2,
            TypeCode = "theory",
            TypeName = "Теория",
            ContentBlocks = new List<ContentBlockEntity>
            {
                new("1")
                {
                    PageId = "page-123",
                    Order = 1,
                    Data = "<p>Test</p>",
                    Type = ContentBlockType.CreateMarkup,
                    CreatedByUser = creator,
                    CreatedBy = creator.Id
                }
            },
            CreatedByUser = creator,
            CreatedBy = creator.Id
        };

        // Act
        var page = mapper.Map(originalEntity);
        var resultEntity = mapper.Map(page);

        // Assert
        resultEntity.Should().BeEquivalentTo(originalEntity, options => options
            .ComparingByMembers<PageEntity>());
    }

    [Test]
    public void Map_WhenPageHasMultipleContentBlocks_ShouldPreserveOrder()
    {
        // Arrange
        var page = new Page("page-1", 1, PageType.Theory)
        {
            Type = PageType.Theory,
            ContentBlocks = new List<PageContentBlock>
            {
                new("1", "page-1", 1, ContentBlockType.CreateMarkup, "First")
                {
                    CreatedBy = UserObjectMother.Create("user1")
                },
                new("2", "page-1", 2, ContentBlockType.CreateImage, "Second")
                {
                    CreatedBy = UserObjectMother.Create("user1")
                },
                new("3", "page-1", 3, ContentBlockType.CreateCode, "Third")
                {
                    CreatedBy = UserObjectMother.Create("user1")
                },
            },
            CreatedBy = UserObjectMother.Create("user1"),
        };

        // Act
        var entity = mapper.Map(page);
        var resultPage = mapper.Map(entity);

        // Assert
        resultPage.ContentBlocks.Select(cb => cb.Order).Should().ContainInOrder(1, 2, 3);
        resultPage.ContentBlocks.Select(cb => cb.Data).Should().ContainInOrder("First", "Second", "Third");
    }

    #endregion

    #region User Audit Mapping Tests

    [Test]
    public void Map_WhenContentBlockHasCreatedBy_ShouldMapCreatedByUser()
    {
        // Arrange
        var contentBlock = new PageContentBlock("1", "page-1", 1, ContentBlockType.CreateMarkup, "data")
        {
            CreatedBy = UserObjectMother.Create()
        };

        // Act
        var entity = mapper.Map(contentBlock);

        // Assert
        entity.CreatedByUser.Should().NotBeNull();
    }

    #endregion
}