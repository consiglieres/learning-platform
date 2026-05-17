using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.Entities.Tasks;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Entities.Base;
using LearningPlatformApi.Persistence.Entities.Page;
using Riok.Mapperly.Abstractions;

namespace LearningPlatformApi.Mapper.Impl;

[Mapper]
internal partial class LessonMapper(
    IDbEntityMapper<Page, string, PageEntity, string> pageMapper,
    IDbEntityMapper<CodingTask, string, CodingTaskEntity, string> codingTaskMapper,
    IDbEntityMapper<TestTask, string, TestTaskEntity, string> testTaskMapper,
    IUserMapper userMapper) : IDbEntityMapper<Lesson, string, LessonEntity, string>
{
    public Lesson Map(LessonEntity entity)
    {
        return new Lesson(entity.Name, entity.LessonOrder, entity.PassThreshold,
            pageMapper.Map(entity.PageEntity), entity.ModuleId,
            userMapper.MapToDomain(entity.CreatedByUser))
        {
            CodingTasks = entity.CodingTasks.Select(codingTaskMapper.Map).ToList(),
            TestTasks = entity.TestTasks.Select(testTaskMapper.Map).ToList(),
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            CreatedBy = userMapper.MapToDomain(entity.CreatedByUser),
            UpdatedAt = entity.UpdatedAt,
            UpdatedBy = entity.UpdatedByUser == null ? null : userMapper.MapToDomain(entity.UpdatedByUser),
            DeletedAt = entity.DeletedAt,
            DeletedBy = entity.DeletedByUser == null ? null : userMapper.MapToDomain(entity.DeletedByUser)
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
            PageEntity = pageMapper.Map(entity.PageContent),
            PageId = entity.PageContent.Id,
            CodingTasks = entity.CodingTasks.Select(codingTaskMapper.Map).ToList(),
            TestTasks = entity.TestTasks.Select(testTaskMapper.Map).ToList(),
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