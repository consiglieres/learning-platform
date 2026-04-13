using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.ValueObjects.Course;
using LearningPlatformApi.Domain.ValueObjects.Page;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Entities.Page;
using Riok.Mapperly.Abstractions;

namespace LearningPlatformApi.Mapper.Impl;

[Mapper]
internal partial class DbMapper(IUserMapper userMapper) : IDbEntityMapper<Course, string, CourseEntity, string>,
    IDbEntityMapper<Page, string, PageEntity, string>
{
    public Course Map(CourseEntity entity)
    {
        return new Course(entity.Title, entity.Description, userMapper.MapToDomain(entity.CreatedByUser))
        {
            Id = entity.Id,
            Categories = entity.Categories.Select(Map).ToList(),
        };
    }

    public CourseEntity Map(Course entity)
    {
        return new CourseEntity(entity.Id)
        {
            Title = entity.Title,
            Description = entity.Description,
            CreatedBy = entity.CreatedBy.UserName,
            Categories = entity.Categories.Select(Map).ToList(),
        };
    }

    public Page Map(PageEntity entity)
    {
        return new Page(entity.Id, new PageType(entity.TypeCode, entity.TypeName), entity.ContentBlocks.Select(Map).ToList());
    }
    
    public PageEntity Map(Page entity)
    {
        return new PageEntity(entity.Id)
        {
            Order = entity.Order,
            TypeCode = entity.Type.Code,
            TypeName = entity.Type.Name,
            ContentBlocks = entity.ContentBlocks.Select(Map).ToList()
        };
    }
    
    public partial PageContentBlock Map(ContentBlockEntity entity);
    
    public partial ContentBlockEntity Map(PageContentBlock entity);

    public TypedCategory Map(CategoryEntity entity)
    {
        return new TypedCategory(new CategoryType(entity.TypeCode, entity.TypeName), new Category(entity.ValueCode, entity.ValueName));
    }

    public CategoryEntity Map(TypedCategory entity)
    {
        return new CategoryEntity
        {
            TypeCode = entity.Type.Code,
            TypeName = entity.Type.Name,
            ValueCode = entity.Value.Code,
            ValueName = entity.Value.Name
        };
    }
    
    public string MapId(string id) => id;
}

