using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.Entities.Tasks;
using LearningPlatformApi.V1.Models.Account.Res;
using LearningPlatformApi.V1.Models.Courses.Req;
using LearningPlatformApi.V1.Models.Courses.Res;
using LearningPlatformApi.V1.Models.Lessons;
using LearningPlatformApi.V1.Models.Lessons.Res;
using LearningPlatformApi.V1.Models.Module.Res;
using LearningPlatformApi.V1.Models.Page;
using LearningPlatformApi.V1.Models.Tasks;
using LearningPlatformApi.V2.Account.Res;

namespace LearningPlatformApi.V1.Mapper;

public interface IV1ResDtoMapper
{
    public V1CourseCategory Map(TypedCategory category);

    V1UserResDto Map(User user);

    V1Course Map(Course course);

    V1CourseShort MapToShort(Course course);

    V1ModuleResDto Map(Module module);

    V1ModuleShortResDto MapToShort(Module course);

    V1LessonResDto Map(Lesson lesson);

    V1LessonShortResDto MapToShort(Lesson course);

    V1TaskShortInfo MapShort(CodingTask task);

    V1TaskShortInfo MapShort(TestTask task);

    V1TestTaskResDto Map(TestTask task);

    V1CodingTaskResDto Map(CodingTask task);

    public V1PageResDto Map(Page page);

    public V1PageContentBlock Map(PageContentBlock page);
}