using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.ValueObjects.Page;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Entities.Page;
using Riok.Mapperly.Abstractions;

[Mapper]
internal partial class PageMapper : IDbEntityMapper<Page, string, PageEntity, string>, 
    IDbEntityMapper<PageContentBlock, string, ContentBlockEntity, string>
{
    private readonly IUserMapper? userMapper;

    public PageMapper(IUserMapper userMapper)
    {
        this.userMapper = userMapper;
    }

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

    #region ContentBlock Mapping

    [MapperIgnoreTarget(nameof(ContentBlockEntity.CreatedByUser))]
    [MapperIgnoreTarget(nameof(ContentBlockEntity.UpdatedByUser))]
    [MapperIgnoreTarget(nameof(ContentBlockEntity.DeletedByUser))]
    [MapProperty(nameof(PageContentBlock.CreatedBy.Id), nameof(ContentBlockEntity.CreatedBy))]
    [MapProperty(nameof(PageContentBlock.UpdatedBy.Id), nameof(ContentBlockEntity.UpdatedBy))]
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

        var page = new Page(entity.Id, entity.Order, MapToPageType(entity.TypeCode, entity.TypeName))
        {
            ContentBlocks = entity.ContentBlocks.Select(Map).ToList(),
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            DeletedAt = entity.DeletedAt
        };

        // Маппинг пользователей аудита
        if (userMapper != null)
        {
            if (entity.CreatedByUser != null) page.CreatedBy = userMapper.MapToDomain(entity.CreatedByUser);

            if (entity.UpdatedByUser != null && entity.UpdatedAt.HasValue)
                page.UpdatedBy = userMapper.MapToDomain(entity.UpdatedByUser);

            if (entity.DeletedByUser != null && entity.DeletedAt.HasValue)
                page.DeletedBy = userMapper.MapToDomain(entity.DeletedByUser);
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
            UpdatedAt = page.UpdatedAt,
            UpdatedBy = page.UpdatedBy?.Id,
            DeletedAt = page.DeletedAt,
            DeletedBy = page.DeletedBy?.Id
        };

        return entity;
    }

    public string MapId(string id)
    {
        return id;
    }

    #endregion
}