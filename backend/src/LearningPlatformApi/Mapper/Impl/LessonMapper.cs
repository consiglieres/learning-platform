using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.Entities.Tasks;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Entities.Base;
using LearningPlatformApi.Persistence.Entities.Page;
using Riok.Mapperly.Abstractions;

namespace LearningPlatformApi.Mapper.Impl;

[Mapper]
internal partial class LessonMapper(
    IDbEntityMapper<Page, string, PageEntity, string> pageMapper,
    IDbEntityMapper<Module, string, ModuleEntity, string> moduleMapper,
    IDbEntityMapper<CodingTask, string, CodingTaskEntity, string> codingTaskMapper,
    IDbEntityMapper<TestTask, string, TestTaskEntity, string> testTaskMapper,
    IUserMapper userMapper) : IDbEntityMapper<Lesson, string, LessonEntity, string>
{
    public Lesson Map(LessonEntity entity)
    {
        return new Lesson(entity.Name, entity.LessonOrder, entity.PassThreshold,
            pageMapper.Map(entity.PageEntity), moduleMapper.Map(entity.Module),
            userMapper.MapToDomain(entity.CreatedByUser))
        {
            Tasks = entity.Tasks.Select<TaskBaseEntity, BaseTask>(x =>
            {
                if (x is CodingTaskEntity codingTaskEntity)
                {
                    return codingTaskMapper.Map(codingTaskEntity);
                }

                if (x is TestTaskEntity testTaskEntity)
                {
                    return testTaskMapper.Map(testTaskEntity);
                }

                throw new ArgumentOutOfRangeException();
            }).ToList(),
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

    public LessonEntity Map(Lesson entity)
    {
        return new LessonEntity(entity.Id)
        {
            Name = entity.Name,
            LessonOrder = entity.LessonOrder,
            PassThreshold = entity.PassThreshold,
            ModuleId = entity.ModuleId,
            Module = moduleMapper.Map(entity.Module),
            PageEntity = pageMapper.Map(entity.PageContent),
            PageId = entity.PageContent.Id,
            Tasks = entity.Tasks.Select<BaseTask, TaskBaseEntity>(x =>
            {
                if (x is CodingTask codingTaskEntity)
                {
                    return codingTaskMapper.Map(codingTaskEntity);
                }

                if (x is TestTask testTaskEntity)
                {
                    return testTaskMapper.Map(testTaskEntity);
                }

                throw new ArgumentOutOfRangeException();
            }).ToList(),
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