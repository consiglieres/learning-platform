using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Entities.Page;
using Riok.Mapperly.Abstractions;

namespace LearningPlatformApi.Mapper.Impl;

[Mapper]
internal partial class ModuleMapper(
    IDbEntityMapper<Page, string, PageEntity, string> pageMapper,
    IDbEntityMapper<Lesson, string, LessonEntity, string> lessonMapper,
    IUserMapper userMapper)
    : IDbEntityMapper<Module, string, ModuleEntity, string>
{
    public Module Map(ModuleEntity entity)
    {
        return new Module(entity.Name, entity.ModuleOrder, entity.CourseId,
            userMapper.MapToDomain(entity.CreatedByUser),
            entity.Lessons.Select(lessonMapper.Map).ToList())
        {
            Page = pageMapper.Map(entity.Page),
            Id = entity.Id,
            Version = new EntityVersion(entity.VersionOrder, entity.Tag),
            CreatedAt = entity.CreatedAt,
            CreatedBy = userMapper.MapToDomain(entity.CreatedByUser),
            UpdatedAt = entity.UpdatedAt,
            UpdatedBy = entity.UpdatedByUser == null ? null : userMapper.MapToDomain(entity.UpdatedByUser),
            DeletedAt = entity.DeletedAt,
            DeletedBy = entity.DeletedByUser == null ? null : userMapper.MapToDomain(entity.DeletedByUser)
        };
    }

    public ModuleEntity Map(Module entity)
    {
        return new ModuleEntity(entity.Id)
        {
            Name = entity.Name,
            ModuleOrder = entity.ModuleOrder,
            Page = pageMapper.Map(entity.Page),
            PageId = entity.Page.Id,
            CourseId = entity.CourseId,
            Lessons = entity.Lessons.Select(lessonMapper.Map).ToList(),
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

    public string MapId(string id)
    {
        return id;
    }
}