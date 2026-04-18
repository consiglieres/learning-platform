using LearningPlatformApi.Domain.Entities;
using LearningPlatformApi.Domain.Entities.Courses;
using LearningPlatformApi.Domain.Entities.Tasks;
using LearningPlatformApi.Domain.ValueObjects;
using LearningPlatformApi.V1.Models.Account.Res;
using LearningPlatformApi.V1.Models.Base;
using LearningPlatformApi.V1.Models.Courses.Req;
using LearningPlatformApi.V1.Models.Courses.Res;
using LearningPlatformApi.V1.Models.Lessons;
using LearningPlatformApi.V1.Models.Module;
using LearningPlatformApi.V1.Models.Tasks;
using LearningPlatformApi.V2.Account.Res;
using Riok.Mapperly.Abstractions;

namespace LearningPlatformApi.V1.Mapper;

[Mapper]
internal partial class V1ResDtoMapper : IV1ResDtoMapper
{
    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    public partial V1UserResDto Map(User user);

    public partial  V1Course Map(Course course);

    public partial V1ModuleResDto Map(Module module);

    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    public partial V1LessonResDto Map(Lesson lesson);

    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    public partial V1TaskShortInfo MapShort(CodingTask task);

    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    public partial  V1TaskShortInfo MapShort(TestTask task);
    
    public partial V1CourseCategory Map(TypedCategory category);
    
    public partial VersionDto Map(EntityVersion version);

    [MapperIgnoreSource(nameof(TestTask.Answer))]
    public partial  V1TestTaskResDto Map(TestTask task);
    
    [MapperIgnoreSource(nameof(CodingTask.TestCode))]
    public partial V1CodingTaskResDto Map(CodingTask task);
}