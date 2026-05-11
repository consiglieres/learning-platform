using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.V1.Models.Lessons.Req;
using LearningPlatformApi.V1.Models.Lessons.Res;

namespace LearningPlatformApi.Services;

public interface ILessonService
{
    Task<V1LessonResDto> CreateAsync(V1CreateLessonReqDto request, User user, CancellationToken cancellationToken);

    Task<V1LessonResDto> UpdateAsync(string id, User user, V1UpdateLessonReqDto request, CancellationToken cancellationToken);
    
    Task<V1LessonResDto> GetByIdAsync(string id, CancellationToken cancellationToken);

    Task DeleteAsync(string id, User user, CancellationToken cancellationToken);

    Task<V1LessonResDto> RestoreAsync(string id, User user, CancellationToken cancellationToken);

    Task<List<V1LessonResDto>> GetLessonsByIdsAsync(List<string> ids, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<V1LessonResDto>> ReorderLessonsAsync(string id, List<string> lessonIds, CancellationToken cancellationToken);
}