using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.Entities.Tasks;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Entities.Page;
using Riok.Mapperly.Abstractions;

namespace LearningPlatformApi.Mapper.Impl;

[Mapper]
internal partial class ModuleMapper(
    IDbEntityMapper<Page, string, PageEntity, string> pageMapper,
    IDbEntityMapper<Module, string, ModuleEntity, string> moduleMapper,
    IDbEntityMapper<CodingTask, string, CodingTaskEntity, string> codingTaskMapper,
    IDbEntityMapper<TestTask, string, TestTaskEntity, string> testTaskMapper,
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
            IntroductionPage = pageMapper.Map(entity.IntroductionPage),
            Id = entity.Id,
            AllVersions = [],
            Version = new EntityVersion(entity.VersionOrder, entity.Tag),
            CreatedAt = entity.CreatedAt,
            CreatedBy = userMapper.MapToDomain(entity.CreatedByUser),
            UpdatedAt = entity.UpdatedAt,
            UpdatedBy = entity.UpdatedByUser == null ? null : userMapper.MapToDomain(entity.UpdatedByUser),
            DeletedAt = entity.DeletedAt,
            DeletedBy = entity.DeletedByUser == null ? null : userMapper.MapToDomain(entity.DeletedByUser),
        };
    }

    public ModuleEntity Map(Module entity)
    {
        return new ModuleEntity(entity.Id)
        {
            Name = entity.Name,
            ModuleOrder = entity.ModuleOrder,
            IntroductionPage = pageMapper.Map(entity.IntroductionPage),
            PageId = entity.IntroductionPage.Id,
            CourseId = entity.CourseId,
            Lessons = entity.Lessons.Select(lessonMapper.Map).ToList(),
            VersionOrder = entity.Version.Order,
            Tag = entity.Version.Tag,
            CreatedAt = entity.CreatedAt,
            CreatedBy = entity.CreatedBy.Id,
            CreatedByUser = userMapper.MapToEntity(entity.CreatedBy),
            UpdatedAt = entity.UpdatedAt,
            UpdatedBy = entity.UpdatedBy?.Id,
            UpdatedByUser = entity.UpdatedBy == null ? null : userMapper.MapToEntity(entity.UpdatedBy),
            DeletedAt = entity.DeletedAt,
            DeletedBy = entity.DeletedBy?.Id,
            DeletedByUser = entity.DeletedBy == null ? null : userMapper.MapToEntity(entity.DeletedBy)
        };
    }

    public string MapId(string id) => id;
}