using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Repositories;
using LearningPlatformApi.Mapper;
using LearningPlatformApi.Persistence.Entities;
using LearningPlatformApi.Persistence.Repositories.Base;
using LearningPlatformApi.V1.Mapper;
using LearningPlatformApi.V1.Models.Lessons.Req;
using LearningPlatformApi.V1.Models.Lessons.Res;

namespace LearningPlatformApi.Services.Impl;

public class LessonService(ILessonRepository lessonRepository, IV1ResDtoMapper resDtoMapper,
    IUnitOfWork unitOfWork, IDbEntityMapper<Lesson, string, LessonEntity, string> lessonMapper)
    : ILessonService
{
    public Task<V1LessonResDto> CreateAsync(V1CreateLessonReqDto request, User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<V1LessonResDto> UpdateAsync(string id, User user, V1UpdateLessonReqDto request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<V1LessonResDto> GetLatestAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<V1LessonResDto> GetByVersionAsync(string id, int versionOrder, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string id, User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<V1LessonResDto> RestoreAsync(string id, User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<V1LessonResDto> RollbackToVersionAsync(string id, int targetVersionOrder, string? reason, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<V1LessonVersionInfoResDto>> GetVersionHistoryAsync(string id, int limit, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<V1LessonResDto>> GetModulesByIdsAsync(List<string> ids, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<V1LessonResDto> ReorderLessonsAsync(string id, List<string> lessonIds, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}