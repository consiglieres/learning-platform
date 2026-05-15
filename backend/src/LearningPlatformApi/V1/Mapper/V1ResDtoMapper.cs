using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Entities.Page;
using LearningPlatformApi.Domain.Entities.Tasks;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.V1.Models.Account.Res;
using LearningPlatformApi.V1.Models.Base;
using LearningPlatformApi.V1.Models.Courses.Req;
using LearningPlatformApi.V1.Models.Courses.Res;
using LearningPlatformApi.V1.Models.Lessons;
using LearningPlatformApi.V1.Models.Lessons.Res;
using LearningPlatformApi.V1.Models.Module.Res;
using LearningPlatformApi.V1.Models.Page;
using LearningPlatformApi.V1.Models.Tasks;
using LearningPlatformApi.V2.Account.Res;
using Riok.Mapperly.Abstractions;

namespace LearningPlatformApi.V1.Mapper;

[Mapper]
internal partial class V1ResDtoMapper : IV1ResDtoMapper
{
    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    public partial V1UserResDto Map(User user);

    public partial V1Course Map(Course course);

    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    public partial V1CourseShort MapToShort(Course course);

    public partial V1ModuleResDto Map(Module module);

    public V1ModuleShortResDto MapToShort(Module course)
    {
        return new V1ModuleShortResDto()
        {
            Id = course.Id,
            Name = course.Name,
            ModuleOrder = course.ModuleOrder,
            CourseId = course.CourseId,
            LessonIds = course.Lessons.Select(x => x.Id).ToArray(),
            IntroductionPageId = course.Page.Id,
            CreatedAt = course.CreatedAt,
            CreatedBy = Map(course.CreatedBy),
            UpdatedAt = course.UpdatedAt,
            UpdatedBy = course.UpdatedBy == null ? null : Map(course.UpdatedBy),
            DeletedAt = course.DeletedAt,
            DeletedBy = course.DeletedBy == null ? null : Map(course.DeletedBy)
        };
    }

    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    public partial V1LessonResDto Map(Lesson lesson);

    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    public partial V1LessonShortResDto MapToShort(Lesson course);

    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    public partial V1TaskShortInfo MapShort(BaseTask task);

    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    public partial V1TaskShortInfo MapShort(CodingTask task);

    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    public partial V1TaskShortInfo MapShort(TestTask task);

    public partial V1CourseCategory Map(TypedCategory category);

    [MapperIgnoreSource(nameof(TestTask.Answer))]
    public partial V1TestTaskResDto Map(TestTask task);

    [MapperIgnoreSource(nameof(CodingTask.TestCode))]
    public partial V1CodingTaskResDto Map(CodingTask task);

    public partial V1PageResDto Map(Page page);

    public partial V1PageContentBlock Map(PageContentBlock page);


    private string MapLists(Lesson lessons)
    {
        return lessons.Id;
    }
}