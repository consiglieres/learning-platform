using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.ValueObjects.Page;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Entities.Page;
using Riok.Mapperly.Abstractions;

[Mapper]
internal partial class PageMapper : IDbEntityMapper<Page, string, PageEntity, string>
{
    private readonly IUserMapper? userMapper;

    public PageMapper(IUserMapper userMapper)
    {
        this.userMapper = userMapper;
    }

    #region ContentBlock Mapping

    [MapProperty(nameof(PageContentBlock.CreatedBy), nameof(ContentBlockEntity.CreatedByUser))]
    [MapProperty(nameof(PageContentBlock.CreatedBy.Id), nameof(ContentBlockEntity.CreatedBy))]
    [MapProperty(nameof(PageContentBlock.UpdatedBy), nameof(ContentBlockEntity.UpdatedByUser))]
    [MapProperty(nameof(PageContentBlock.UpdatedBy.Id), nameof(ContentBlockEntity.UpdatedBy))]
    [MapProperty(nameof(PageContentBlock.DeletedBy), nameof(ContentBlockEntity.DeletedByUser))]
    [MapProperty(nameof(PageContentBlock.DeletedBy.Id), nameof(ContentBlockEntity.DeletedBy))]
    public partial ContentBlockEntity Map(PageContentBlock contentBlock);

    [MapProperty(nameof(ContentBlockEntity.CreatedByUser), nameof(PageContentBlock.CreatedBy))]
    [MapProperty(nameof(ContentBlockEntity.UpdatedByUser), nameof(PageContentBlock.UpdatedBy))]
    [MapProperty(nameof(ContentBlockEntity.DeletedByUser), nameof(PageContentBlock.DeletedBy))]
    public partial PageContentBlock Map(ContentBlockEntity entity);

    #endregion

    #region Page Mapping (Full Manual)

    public Page Map(PageEntity entity)
    {
        if (entity == null) return null!;

        var page = new Page(entity.Id)
        {
            Order = entity.Order,
            Type = MapToPageType(entity.TypeCode, entity.TypeName),
            ContentBlocks = entity.ContentBlocks?.Select(Map).ToList() ?? new List<PageContentBlock>(),
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            DeletedAt = entity.DeletedAt
        };

        // Маппинг пользователей аудита
        if (userMapper != null)
        {
            if (entity.CreatedByUser != null)
            {
                page.CreatedBy = userMapper.MapToDomain(entity.CreatedByUser);
            }

            if (entity.UpdatedByUser != null && entity.UpdatedAt.HasValue)
            {
                page.UpdatedBy = userMapper.MapToDomain(entity.UpdatedByUser);
            }

            if (entity.DeletedByUser != null && entity.DeletedAt.HasValue)
            {
                page.DeletedBy = userMapper.MapToDomain(entity.DeletedByUser);
            }
        }

        return page;
    }

    public PageEntity Map(Page page)
    {
        if (page == null) return null!;

        var entity = new PageEntity(page.Id)
        {
            Order = page.Order,
            TypeCode = page.Type.Code,
            TypeName = page.Type.Name,
            ContentBlocks = page.ContentBlocks?.Select(Map).ToList() ?? new List<ContentBlockEntity>(),
            CreatedAt = page.CreatedAt,
            CreatedBy = page.CreatedBy.Id,
            CreatedByUser = userMapper.MapToEntity(page.CreatedBy),
            UpdatedAt = page.UpdatedAt,
            UpdatedBy = page.UpdatedBy?.Id,
            UpdatedByUser = page.UpdatedBy == null ? null : userMapper.MapToEntity(page.UpdatedBy),
            DeletedAt = page.DeletedAt,
            DeletedBy = page.DeletedBy?.Id,
            DeletedByUser = page.DeletedBy == null ? null : userMapper.MapToEntity(page.DeletedBy),
        };

        return entity;
    }

    public string MapId(string id)
    {
        return id;
    }

    #endregion

    #region Type Mapping Methods

    private PageType MapToPageType(string typeCode, string typeName)
    {
        return typeCode switch
        {
            "intro" => PageType.Introduction,
            "theory" => PageType.Theory,
            "task" => PageType.Task,
            _ => new PageType(typeCode, typeName)
        };
    }

    #endregion
}