using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Entities.Page;
using Riok.Mapperly.Abstractions;

namespace LearningPlatformApi.Mapper.Impl;

[Mapper]
internal partial class CourseMapper(
    IDbEntityMapper<Page, string, PageEntity, string> pageMapper,
    IDbEntityMapper<Module, string, ModuleEntity, string> moduleMapper,
    IUserMapper userMapper) : IDbEntityMapper<Course, string, CourseEntity, string>
{
    public Course Map(CourseEntity entity)
    {
        return new Course(entity.Title, entity.Description, userMapper.MapToDomain(entity.CreatedByUser))
        {
            Categories = entity.Categories.Select(x =>
                    new TypedCategory(x.TypeName, x.ValueName))
                .ToList(),
            Modules = entity.Modules.Select(moduleMapper.Map).ToList(),
            IntroductionPage = pageMapper.Map(entity.IntroductionPage),
            ModerationComment = entity.ModerationComment,
            SubmittedForModerationAt = entity.SubmittedForModerationAt,
            SubmittedBy = entity.SubmittedByUser == null ? null : userMapper.MapToDomain(entity.SubmittedByUser),
            PublishedAt = entity.PublishedAt,
            PublishedBy = entity.PublishedByUser == null ? null : userMapper.MapToDomain(entity.PublishedByUser),
            Status = entity.Status,
            Id = entity.Id,
            Version = new EntityVersion(entity.VersionOrder, entity.Tag),
            CreatedAt = entity.CreatedAt,
            CreatedBy = userMapper.MapToDomain(entity.CreatedByUser),
            UpdatedAt = entity.UpdatedAt,
            UpdatedBy = entity.UpdatedByUser == null ? null : userMapper.MapToDomain(entity.UpdatedByUser),
            DeletedAt = entity.DeletedAt,
            DeletedBy = entity.DeletedByUser == null ? null : userMapper.MapToDomain(entity.DeletedByUser),
        };
    }

    public CourseEntity Map(Course entity)
    {
        return new CourseEntity(entity.Id)
        {
            Title = entity.Title,
            Description = entity.Description,
            Categories = entity.Categories.Select(x => new CategoryEntity
            {
                TypeName = x.Type,
                ValueName = x.Value
            }).ToList(),
            Modules = entity.Modules.Select(moduleMapper.Map).ToList(),
            PageId = entity.IntroductionPage.Id,
            IntroductionPage = pageMapper.Map(entity.IntroductionPage),
            ModerationComment = entity.ModerationComment,
            SubmittedForModerationAt = entity.SubmittedForModerationAt,
            SubmittedBy = entity.SubmittedBy?.Id,
            PublishedAt = entity.PublishedAt,
            PublishedBy = entity.PublishedBy?.Id,
            VersionOrder = entity.Version.Order,
            Tag = entity.Version.Tag,
            CreatedAt = entity.CreatedAt,
            CreatedBy = entity.CreatedBy.Id,
            UpdatedAt = entity.UpdatedAt,
            UpdatedBy = entity.UpdatedBy?.Id,
            DeletedAt = entity.DeletedAt,
            DeletedBy = entity.DeletedBy?.Id,
        };
    }

    public string MapId(string id) => id;
}